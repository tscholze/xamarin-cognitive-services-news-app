using System;
using System.ComponentModel;
using Xamarin.Forms;

using Invia.News.Models;
using Invia.News.ViewModels;

namespace Invia.News.Views
{
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

            // Setup messaging center.
            MessagingCenter.Subscribe<ArticlesViewModel, string>(this, ArticlesViewModel.NOTIFICATION_ALERT_REQUESTED, OnAlertRequested);
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

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            // Get the underlying article.
            var article = args.SelectedItem as Article;

            // Navigate to detail page.
            await Navigation.PushAsync(new ArticlePage(new ArticleViewModel(article)));

            // Reset selected item.
            ArticlesListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            // Open a modal. 
            await Navigation.PushModalAsync(new NavigationPage(new AboutPage()));
        }

        #endregion

        #region Life cycle
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Load articles if required.
            if (!viewModel.HasArticles)
            {
                viewModel.LoadArticlesCommand.Execute(null);
            }
        }

        #endregion
    }
}