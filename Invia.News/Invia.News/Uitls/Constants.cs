using System;
using System.Collections.Generic;
using System.Text;

namespace Invia.News.Uitls
{
    /// <summary>
    /// Defines app-global constants.
    /// </summary>
    public static class Constants
    {
        #region Urls

        public readonly static string FEED_URL = "https://rss.dw.com/rdf/rss-en-top";

        #endregion

        #region Notification keys

        /// <summary>
        /// The notification identifier for adding a new feed item failed.
        /// </summary>
        public readonly static string NOTIFICATION_ID_FEED_ITEM_ADD_FAILED = "NOTIFICATION_ID_FEED_ITEM_ADD_FAILED";

        #endregion
    }
}
