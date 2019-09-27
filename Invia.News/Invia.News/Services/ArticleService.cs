using Invia.News.Models;
using Invia.News.Uitls;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Invia.News.Services
{
    /// <summary>
    /// Atricle service to provide with sentiment score enriched
    /// items from DeutscheWelle feed.
    /// </summary>
    sealed class ArticleService
    {
        #region Constants

        /// <summary>
        /// Azure text analystics subscription key.
        /// </summary>
        private static readonly string ANALYTICS_SUBSCRIPTION_KEY = "";

        /// <summary>
        /// Azure text analytics endpoint.
        /// </summary>
        private static readonly string ANALYTICS_ENDPOINT = "";

        /// <summary>
        /// Article feed endpoint.
        /// </summary>
        private readonly static string FEED_ENDPOINT = "https://rss.dw.com/rdf/rss-en-top";

        /// <summary>
        /// Threshold that indicates a happy article.
        /// </summary>
        private readonly static double HAPPY_SCORE_THRESHOLD = 0.75;

        #endregion

        #region Private member

        /// <summary>
        /// List of already requested articles.
        /// </summary>
        private List<Article> cachedArticles = new List<Article>();

        #endregion

        #region Public member

        public static ArticleService Instance { get; } = new ArticleService();

        #endregion

        #region Init

        private ArticleService()
        {
            // place for instance initialization code
        }

        #endregion

        #region Public helper

        /// <summary>
        /// Gets all articles with a happy sentiment score.
        /// </summary>
        /// <param name="forceReload">If true, cache wil be ignored.</param>
        /// <returns>List of happy articles.</returns>
        public async Task<List<Article>> GetHappyArticlesAsync(bool forceReload = false)
        {
            return (await GetArticlesAsync(forceReload)).Where(a => a.SentimentScore > HAPPY_SCORE_THRESHOLD).ToList();
        }

        /// <summary>
        /// Gets all articles with a sad sentiment score.
        /// </summary>
        /// <param name="forceReload">If true, cache wil be ignored.</param>
        /// <returns>List of sad articles.</returns>
        public async Task<List<Article>> GetSadArticlesAsync(bool forceReload = false)
        {
            return (await GetArticlesAsync(forceReload)).Where(a => a.SentimentScore <= HAPPY_SCORE_THRESHOLD).ToList();
        }

        #endregion

        #region Private helper

        /// <summary>
        /// Gets the url's feed posts.
        /// </summary>
        /// <param name="forceReload">If true, the cache will not be used.</param>
        /// <returns>The feed's posts.</returns>
        private async Task<List<Article>> GetArticlesAsync(bool forceReload = false)
        {
            // Use cached articles if possible.
            if (cachedArticles.Count != 0 && !forceReload)
            {
                return cachedArticles;
            }

            // Setup web client.
            WebClient client = new WebClient
            {
                CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
            };

            // Download rss file.
            var rssString = client.DownloadString(FEED_ENDPOINT);

            // Setup xml document parser.
            XDocument doc = XDocument.Parse(rssString);
            XNamespace rdf = "http://www.w3.org/1999/02/22-rdf-syntax-ns#";
            XNamespace dwsyn = "http://rss.dw.com/syndication/dwsyn/";
            XNamespace dc = "http://purl.org/dc/elements/1.1/";
            XNamespace purl = "http://purl.org/rss/1.0/";

            // Parse list of posts.
            var items = doc.Element(rdf + "RDF").Elements(purl + "item");
            var articles = items.Select(item => new Article
            {
                Guid = item.Element(dwsyn + "contentID").Value,
                LanguageCode = item.Element(dc + "language").Value,
                Title = item.Element(purl + "title").Value,
                Link = item.Element(purl + "link").Value,
                Description = item.Element(purl + "description").Value
            }).ToList();

            // Add sentiment analysis.
            articles = await AddSentiments(articles);

            // Store locally
            cachedArticles = articles;

            // Return created articles from parsed document.
            return articles;
        }

        /// <summary>
        /// Adds sentiment to the given list and returns it.
        /// </summary>
        /// <param name="articles">Articles to enrich.</param>
        /// <returns>Enriched list of articles.</returns>
        async Task<List<Article>> AddSentiments(List<Article> articles)
        {
            // Ensure all required configuration information are available.
            if (ANALYTICS_SUBSCRIPTION_KEY == string.Empty || ANALYTICS_ENDPOINT == string.Empty)
            {
                //throw new MissingMemberException("Azure analystics constants empty. Please set them.");
                return articles;
            }

            // Setup text analytics client.
            var credentials = new ApiKeyServiceClientCredentials(ANALYTICS_SUBSCRIPTION_KEY);
            TextAnalyticsClient client = new TextAnalyticsClient(credentials)
            {
                Endpoint = ANALYTICS_ENDPOINT
            };

            // Build batch inputs based on articles.
            var batchInput = new MultiLanguageBatchInput
            {
                Documents = articles.Select(a => new MultiLanguageInput
                {
                    Id = a.Guid,
                    Language = a.LanguageCode,
                    Text = a.Description
                }).ToList()
            };

            // Get sentiment analytics from Azure.
            var result = await client.SentimentBatchAsync(batchInput);
            
            // If any error occures, return the non enriched article list.
            if(result.Errors.Count > 0)
            {
                Console.WriteLine("Error occured");
                return articles;
            }

            // Enrich atricles with found sentiment score.
            foreach (var document in result.Documents)
            {
                articles.First(a => a.Guid == document.Id).SentimentScore = document.Score ?? -1;
            }

            // Return enriched articles.
            return articles;
        }

        #endregion
    }
}