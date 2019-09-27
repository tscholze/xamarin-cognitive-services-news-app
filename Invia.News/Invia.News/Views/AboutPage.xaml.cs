using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Invia.News.Views
{
    /// <summary>
    /// Will present 'about' information of the app.
    /// It provides a embedded webview which opens external links
    /// in the device's browser.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        #region Private constants

        /// <summary>
        /// Underlying html content of the about page.
        /// </summary>
        private readonly string HTML_CONTENT = @"
            <html>
            <head>
                <meta charset='UTF-8'>
                <meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=1'>
                <title>Information</title>

                <style>
                    body 
                    {
                        font-family: -apple-system, BlinkMacSystemFont, sans-serif;
                        line-height: 150%;
                        color: #474747;
                    }

                    a, a:active, a:visited, a:hover
                    {
                        color: #FF7F00;
                    }
                </style>


              </head>
              <body>
                  <p>
                      <strong>What is it?</strong><br />
                      <i>Invia.News</i> is an Xamarin app that uses the English feed of the great <a href='http://www.dw.com/en/top-stories/s-9097'>Deutsche Welle</a> news feed. 
                      It enriches the feed articles with a happiness score provided by <a href='https://www.microsoft.com/cognitive-services/en-us/apis'>Microsoft Cognitive Services</a>.
                      That means, that the user is able to choose wether the user want to read happy or unhappy articles. False positive articles are possbile.
                  </p>
     
                  <p>
                      <strong>Why?</strong><br />
                      This app is targeted to work as a play ground for the <a href='https://www.microsoft.com/cognitive-services/en-us/apis'>Microsoft Cognitive</a> services. 
                      There is no commercial purpose behind this usecase.
                  </p>

                  <p>
                     <strong>Follow-up inks</strong>
                      <ul>
                          <li>App's source code on <a href='https://github.com/tscholze/Invia.News'>Github</a></li>
                          <li>Microsoft <a href='https://www.microsoft.com/cognitive-services/en-us/apis'>Cognitive Services</a> API documentation</li>
                          <li><a href='https://dotnet.microsoft.com/apps/xamarin'>Xamarin</a> Homepage</li>
                          <li><a href='https://materialdesignicons.com'>materialdesignicons.com</a> as source for all of the icons</li>
                      </ul>
                  </p>
              </body>
            </html>";

        #endregion

        #region Init

        /// <summary>
        /// Init.
        ///
        /// Will load local about content.
        /// </summary>
        public AboutPage()
        {
            InitializeComponent();

            // Setup view.
            Title = "About Invia.News";

            // Setup web view.
            var source = new HtmlWebViewSource
            {
                Html = HTML_CONTENT
            };

            WebView.Navigating += WebView_Navigating;
            WebView.Source = source;
        }

        #endregion


        #region Event handler

        /// <summary>
        /// Raised if user taps on the close icon.
        /// </summary>
        /// <param name="sender">Requesting button.</param>
        /// <param name="e">Event args.</param>
        async void CloseItem_Clicked(object sender, EventArgs e)
        {
            // Close a modal. 
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Raised if the web view will navigate.
        /// Used to open urls in device's browser.
        /// </summary>
        /// <param name="sender">Requesting web view.</param>
        /// <param name="e">Event args.</param>
        private void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url.StartsWith("file://", StringComparison.Ordinal))
            {
                return;
            }

            Device.OpenUri(new Uri(e.Url));
            e.Cancel = true;
        }

        #endregion
    }
}