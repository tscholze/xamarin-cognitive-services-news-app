using Invia.News.Models;
using Invia.News.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Invia.News.ViewModels
{
    public class ArticlesViewModel : BaseViewModel
    {
        #region Public member

        /// <summary>
        /// Title of the page.
        /// </summary>
        public string Title
        {
            set
            {
                title = value;
                OnPropertyChanged();
            }

            get
            {
                return title;
            }
        }

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

        /// <summary>
        /// Backing field of the Title.
        /// </summary>
        private string title;

        /// <summary>
        /// Backing field for underlying articles.
        /// </summary>
        private List<Article> articles = new List<Article>();

        /// <summary>
        /// Determines if happy or sad articles will be shown.
        /// </summary>
        private readonly bool showHappy;

        #endregion

        #region Init

        /// <summary>
        /// Initializer.
        ///
        /// It will setup the view model.
        /// </summary>
        public ArticlesViewModel(bool showHappy = true)
        {
            // Setup members
            this.showHappy = showHappy;
            Title = showHappy ? "Happy News" : "Sad News";

            // Setup commands
            LoadArticlesCommand = new Command(async () => await LoadArticlesAsync());
        }

        #endregion

        #region Private helper

        /// <summary>
        /// Loads atricles async from the webserver.
        /// </summary>
        async Task LoadArticlesAsync()
        {
            // Update articles for page.
            Articles = showHappy 
                ? await ArticleService.Instance.GetHappyArticlesAsync() 
                : await ArticleService.Instance.GetSadArticlesAsync();

            // If no articles found, present an alert.
            if(HasArticles == false)
            {
                var mode = showHappy ? "happy" : "sad";
                await Application.Current.MainPage.DisplayAlert("No articles found", $"There are currently no {mode} articles.", "OK");
            }
        }

        #endregion
    }
}
