using Invia.News.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Invia.News.Services
{
    class ArticleService
    {
        #region Public helper

        /// <summary>
        /// Gets the url's feed posts.
        /// </summary>
        /// <returns>The feed's posts.</returns>
        /// <param name="urlString">URL string.</param>
        public static List<Article> GetArticles(string urlString)
        {
            // Setup web client.
            WebClient client = new WebClient
            {
                CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
            };

            // Download rss file.
            var rssString = client.DownloadString(urlString);

            // Setup xml document parser.
            XDocument doc = XDocument.Parse(rssString);
            XNamespace rdf = "http://www.w3.org/1999/02/22-rdf-syntax-ns#";
            XNamespace dc = "http://purl.org/dc/elements/1.1/";
            XNamespace purl = "http://purl.org/rss/1.0/";

            // Parse list of posts.
            var items = doc.Element(rdf + "RDF").Elements(purl + "item");
            var articles = items.Select(item => new Article 
            { 
                Title = item.Element(purl + "title").Value,
                Link = item.Element(purl + "link").Value,
                Description = item.Element(purl + "description").Value
            }).ToList();

            // Return created articles from parsed document.
            return articles;
        }

        #endregion
    }
}
