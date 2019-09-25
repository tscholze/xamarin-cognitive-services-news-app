using Invia.News.Models;
using Invia.News.Services;
using Invia.News.Uitls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Invia.News.ViewModels
{
    class ArticlesViewModel: BaseViewModel
    {
        private List<Article> articles = new List<Article>();
        public List<Article> Articles
        {
            set
            {
                articles = value;
                OnPropertyChanged();
            }

            get
            {
                return articles;
            }
        }

        public bool HasArticles
        {
            get
            {
                return articles.Count != 0;
            }
        }

        /// <summary>
        /// Gets the load feed command.
        /// </summary>
        /// <value>The load feed command.</value>
        public ICommand LoadArticlesCommand { get; private set; }

        #region Init

        public ArticlesViewModel()
        {
            // Setup commands
            LoadArticlesCommand = new Command(LoadArticlesAsync);
        }

        #endregion

        #region Private helper
        void LoadArticlesAsync()
        {
            articles = ArticleService.GetArticles(Constants.FEED_URL);
        }

        #endregion
    }
}
