using Xamarin.Forms;

namespace Invia.News
{
    /// <summary>
    /// App's entry point.
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
