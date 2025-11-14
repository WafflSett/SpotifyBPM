using FlagGame.Classes;
using SpotifyBPM.Classes;
using SpotifyBPM.ViewModels;
using System.Text.Json;

namespace SpotifyBPM.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel mv)
        {
            string? storageResult = Task.Run(async () => await SecureStorage.Default.GetAsync("oauth_token")).Result;
            if (storageResult!=null)
            {
                TokenResponse? oauthToken = JsonSerializer.Deserialize<TokenResponse?>(storageResult);
                App.AccessToken = oauthToken;
            }
            if (!CheckToken())
            {
                InitializeComponent();
                this.BindingContext = mv;
            }
            else {
                Shell.Current.GoToAsync("//AppPage");
            }
        }

        private bool CheckToken() {
            if (App.AccessToken != null)
            {
                // check if token expired yet
                if (App.AccessToken.recievedAt.AddSeconds(App.AccessToken.expires_in) < DateTime.Now)
                {
                    //refresh token
                    App.AccessToken = Task.Run(async () => await HttpCommunication.RefreshAccessToken(App.AccessToken.refresh_token)).Result;
                    if (App.AccessToken != null)
                    {
                        return true;
                    }
                }
                else { 
                    return true;
                }
            }
            return false;
        }
    }

}
