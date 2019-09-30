using System;
using System.ComponentModel;
using Xamarin.Forms;

using Invia.News.Models;
using Invia.News.ViewModels;

namespace Invia.News.Views
{
    /// <summary>
    /// Will present a list of happy or sad atricles.
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class ArticlesPage : ContentPage
    {
        #region Private member

        /// <summary>
        /// Underlying view model.
        /// </summary>
        readonly ArticlesViewModel viewModel;

        #endregion

        #region Init

        /// <summary>
        /// Sets up the page with a binding context.
        /// </summary>
        public ArticlesPage(ArticlesViewModel viewModel)
        {
            InitializeComponent();

            // Setup binding context.
            BindingContext = this.viewModel = viewModel;
        }

        #endregion

        #region Life cycle

        /// <summary>
        /// Raised if the page appears.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Subscribe to view model notifications.
            MessagingCenter.Subscribe<ArticlesViewModel, string>(this, ArticlesViewModel.NOTIFICATION_ALERT_REQUESTED, OnAlertRequested);

            // Load articles if required.
            if (!viewModel.HasArticles)
            {
                viewModel.LoadArticlesCommand.Execute(null);
            }
        }

        /// <summary>
        /// Raised if the page disappears.
        /// </summary>
        protected override void OnDisappearing()
        {
            // Unsubscribe from view model notifications.
            MessagingCenter.Unsubscribe<ArticlesViewModel, string>(this, ArticlesViewModel.NOTIFICATION_ALERT_REQUESTED);

            base.OnDisappearing();
        }


        #endregion

        #region Event handler

        /// <summary>
        /// Raised if an alert has been requested.
        /// </summary>
        /// <param name="sender">Requesting view model.</param>
        /// <param name="message">Message to show.</param>
        void OnAlertRequested(ArticlesViewModel sender, String message)
        {
            DisplayAlert("Information", message, "OK");
        }

        /// <summary>
        /// Raised if user selects an item in list view.
        /// </summary>
        /// <param name="sender">Requesting list view</param>
        /// <param name="e">Event args.</param>
        async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            // Get the underlying article.
            var article = e.Item as Article;

            // Navigate to detail page.
            await Navigation.PushAsync(new ArticlePage(new ArticleViewModel(article)));

            // Reset selected item.
            ArticlesListView.SelectedItem = null;
        }

        /// <summary>
        /// Raised if user taps on the about icon.
        /// </summary>
        /// <param name="sender">Requesting button.</param>
        /// <param name="e">Event args.</param>
        async void AddItem_Clicked(object sender, EventArgs e)
        {
            // Open a modal. 
            await Navigation.PushModalAsync(new NavigationPage(new AboutPage()));
        }

        #endregion
    }
}