using Invia.News.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Invia.News.Views
{
    /// <summary>
    /// Will present an article's content web site.
    /// It provides a embedded webview which opens external links
    /// in the device's browser.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArticlePage : ContentPage
    {
        #region Private member

        /// <summary>
        /// Underlying vie wmodel.
        /// </summary>
        readonly ArticleViewModel viewModel;

        /// <summary>
        /// Determines if the web view has intially navigated.
        /// </summary>
        bool hasNavigated;

        #endregion

        #region Init

        /// <summary>
        /// Init.
        ///
        /// Will set and use the view model.
        /// </summary>
        /// <param name="viewModel"></param>
        public ArticlePage(ArticleViewModel viewModel)
        {
            InitializeComponent();

            // Setup binding context.
            BindingContext = this.viewModel = viewModel;

            // Setup web view.
            WebView.Navigated += WebView_Navigated;
            WebView.Navigating += WebView_Navigating;
        }

        #endregion

        #region Event handler

        /// <summary>
        /// Raised when the page has been initally completely navigated.
        /// </summary>
        /// <param name="sender">Requesting web view</param>
        /// <param name="e">Event args.</param>
        private void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            hasNavigated = true;
        }

        /// <summary>
        /// Raised if the web view will navigate.
        /// Used to open urls in device's browser.
        /// </summary>
        /// <param name="sender">Requesting web view.</param>
        /// <param name="e">Event args.</param>
        private void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            // Ensure that it is no redirect.
            if (!hasNavigated || viewModel.IsNearlySameUrl(e.Url))
            {
                return;
            }

            Device.OpenUri(new Uri(e.Url));
            e.Cancel = true;
        }

        #endregion
    }
}