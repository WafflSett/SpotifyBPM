using SpotifyBPM.Pages;

namespace SpotifyBPM
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AuthPage), typeof(AuthPage));

        }
    }
}
