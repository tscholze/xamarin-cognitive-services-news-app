using System;

namespace Invia.News.Models
{
    /// <summary>
    /// Model of an Deutsche Welle article.
    /// </summary>
    public class Article
    {
        /// <summary>
        /// Gets or sets the GUID.
        /// </summary>
        /// <value>The GUID.</value>
        public string Guid { get; set; }

        /// <summary>
        /// Language code.
        /// </summary>
        public string LanguageCode { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description / abstract.
        /// </summary>
        /// <value>The description / abstract.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the link to the web page.
        /// </summary>
        /// <value>The link to the web page.</value>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the publish date.
        /// </summary>
        /// <value>The publish date.</value>
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// Gets the happeyness score in percent of 1.
        /// </summary>
        public double SentimentScore { get; set; }
    }
}
