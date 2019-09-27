using System.Linq;
using Invia.News.Models;

namespace Invia.News.ViewModels
{
    /// <summary>
    /// View model behind an ArticlePage.
    /// </summary>
    public class ArticleViewModel: BaseViewModel
    {
        #region Public members

        /// <summary>
        /// Html source (link) of the atricle.
        /// </summary>
        public string HtmlSource
        {
            set
            {
                htmlSource = value;
                OnPropertyChanged();
            }

            get
            {
                return htmlSource;
            }
        }

        #endregion

        #region Private member

        /// <summary>
        /// Backing field for `HtmlSource`.
        /// </summary>
        private string htmlSource;

        #endregion

        #region Init

        /// <summary>
        /// Init.
        ///
        /// Will set the html source.
        /// </summary>
        /// <param name="article"></param>
        public ArticleViewModel(Article article)
        {
            HtmlSource = article.Link;
        }

        #endregion

        #region Public helper

        public bool IsNearlySameUrl(string url)
        {
            // 1. Check if url is exactly the same.
            if(HtmlSource == url)
            {
                return true;
            }

            // 2. Check if last parameter equals the source
            // This could happen due to internal redirects to mobile
            // pages (dw.com -> m.dw.com).
            string[] seperator =  {".com"};
            var lastSegmentsSource = HtmlSource.Split(seperator, System.StringSplitOptions.RemoveEmptyEntries).Last();
            if (url.EndsWith(lastSegmentsSource, System.StringComparison.Ordinal))
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}
