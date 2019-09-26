using Invia.News.ViewModels;
using Invia.News.Views;

using Xamarin.Forms;

namespace Invia.News
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Create items.
            var happyTab = new ShellContent 
            {
                Title = "Happy",
                Icon = "tab_feed.png",
                Content = new ArticlesPage(new ArticlesViewModel(true))
            };

            var sadTab = new ShellContent
            {
                Title = "Sad",
                Icon = "tab_feed.png",
                Content = new ArticlesPage(new ArticlesViewModel(false))
            };

            // Setup tabbar content.
            TabBar.Items.Add(happyTab);
            TabBar.Items.Add(sadTab);
        }
    }
}
