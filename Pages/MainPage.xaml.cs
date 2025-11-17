using SpotifyBPM.Classes;
using SpotifyBPM.Managers;
using SpotifyBPM.ViewModels;
using System.Text.Json;

namespace SpotifyBPM.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel mv)
        {
            TokenManager.LoadToken();
            if (!TokenManager.CheckToken())
            {
                InitializeComponent();
                this.BindingContext = mv;
            }
            else {
                Shell.Current.GoToAsync("//AppPage");
            }
        }
    }
}
