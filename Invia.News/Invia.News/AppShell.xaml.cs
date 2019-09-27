using Invia.News.ViewModels;
using Invia.News.Views;

using Xamarin.Forms;

namespace Invia.News
{
    /// <summary>
    /// Shell of the app.
    ///
    /// It will setup the tab bar.
    /// </summary>
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Create items.
            var happyTab = new ShellContent 
            {
                Title = "Happy",
                Icon = "tab_happy.png",
                Content = new ArticlesPage(new ArticlesViewModel(true))
            };

            var sadTab = new ShellContent
            {
                Title = "Sad",
                Icon = "tab_sad",
                Content = new ArticlesPage(new ArticlesViewModel(false))
            };

            // Setup tabbar content.
            TabBar.Items.Add(happyTab);
            TabBar.Items.Add(sadTab);

            // mdi-emoticon-sad-outline mdi-emoticon-happy-outline
        }
    }
}
