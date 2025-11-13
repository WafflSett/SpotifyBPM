using FlagGame.Classes;
using SpotifyBPM.ViewModels;

namespace SpotifyBPM.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel mv)
        {
            InitializeComponent();
            this.BindingContext = mv;
            CheckToken();
        }

        private void CheckToken() {
            if (App.AccessToken != null)
            {
                // check if token expired yet
                if (App.AccessToken.recievedAt.AddSeconds(App.AccessToken.expires_in) < DateTime.Now)
                {
                    //refresh token
                    App.AccessToken = Task.Run(async () => await HttpCommunication.RefreshAccessToken(App.AccessToken.refresh_token)).Result;
                    if (App.AccessToken!=null)
                    {
                        Shell.Current.GoToAsync("//AppPage");
                    }
                }
            }
        }
    }

}
