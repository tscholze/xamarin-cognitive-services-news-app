using Invia.News.Models;
using Invia.News.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Invia.News.ViewModels
{
    /// <summary>
    /// View model behind an ArticlesPage.
    /// </summary>
    public class ArticlesViewModel : BaseViewModel
    {
        #region Public constants 

        /// <summary>
        /// Notification identifier if the viewmodel requests an alert.
        /// </summary>
        public static readonly string NOTIFICATION_ALERT_REQUESTED = "notification-alert-requested";

        #endregion

        #region Public member

        /// <summary>
        /// Determines if page is refreshing.
        /// </summary>
        public bool IsRefreshing
        {
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }

            get
            {
                return isRefreshing;
            }
        }

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

        public ICommand RefreshArticlesCommand { get; private set; }

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
        /// Backing field for IsRefreshing.
        /// </summary>
        private bool isRefreshing;

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
            LoadArticlesCommand = new Command(async () => await LoadArticlesAsync(false));
            RefreshArticlesCommand = new Command(async () => await LoadArticlesAsync(true));

        }

        #endregion

        #region Private helper

        /// <summary>
        /// Loads atricles async from the webserver.
        /// </summary>
        async Task LoadArticlesAsync(bool forceReload)
        {
            // Set states.
            IsRefreshing = true;

            // Update articles for page.
            Articles = showHappy 
                ? await ArticleService.Instance.GetHappyArticlesAsync(forceReload) 
                : await ArticleService.Instance.GetSadArticlesAsync(forceReload);

            // Update states
            IsRefreshing = false;

            // If no articles found, request an alert.
            if (HasArticles == false)
            {
                var mode = showHappy ? "happy" : "sad";
                var message = $"There are currently no {mode} articles.";
                MessagingCenter.Send(this, NOTIFICATION_ALERT_REQUESTED, message);
            }
        }

        #endregion
    }
}
