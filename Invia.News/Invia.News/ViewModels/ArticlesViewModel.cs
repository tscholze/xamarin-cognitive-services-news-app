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
        #region Public member

        /// <summary>
        /// List of all atricles.
        /// </summary>
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

        /// <summary>
        /// Determines if articles are already present.
        /// </summary>
        public bool HasArticles
        {
            get
            {
                return articles.Count != 0;
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets the load feed command.
        /// </summary>
        /// <value>The load feed command.</value>
        public ICommand LoadArticlesCommand { get; private set; }

        #endregion

        #region Private members

        private List<Article> articles = new List<Article>();

        #endregion

        #region Init

        /// <summary>
        /// Initializer.
        ///
        /// It will setup the view model.
        /// </summary>
        public ArticlesViewModel()
        {
            // Setup commands
            LoadArticlesCommand = new Command(async () => await LoadArticlesAsync());
        }

        #endregion

        #region Private helper

        /// <summary>
        /// Loads atricles async from the webserver.
        /// </summary>
        async void LoadArticlesAsync()
        {
            Articles = await ArticleService.GetArticlesAsync();
        }

        #endregion
    }
}
