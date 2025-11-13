using SpotifyBPM.Classes;

namespace SpotifyBPM
{
    public partial class App : Application
    {
        public static TokenResponse? AccessToken { get; set; }
        public static string ClientId = "e5e2e9166e354db89146dffe249ba697";
        
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}
