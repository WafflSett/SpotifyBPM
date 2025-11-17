using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpotifyBPM.Classes;
using SpotifyBPM.Managers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using static System.Net.WebRequestMethods;

namespace SpotifyBPM.ViewModels
{
    public partial class AuthViewModel : ObservableObject
    {
        [ObservableProperty]
        public string webSource;
        public string CodeChallange {  get; set; }
        public string Verifier {  get; set; }
        private string authCode;
        private string redirectUri = "https://wafflsett.github.io/SpotifyBPM/";
        private string scope = "playlist-read-private user-library-read";

        public void StartBrowser() {
            UriBuilder authUri = new UriBuilder("https://accounts.spotify.com/authorize");
            authUri.Query = $"response_type=code&client_id={App.ClientId}&scope={HttpUtility.UrlEncode(scope)}&code_challenge_method=S256&code_challenge={CodeChallange}&redirect_uri={redirectUri}";
            WebSource = authUri.Uri.AbsoluteUri;
        }

        [RelayCommand]
        public async Task WebViewNavigated(WebNavigatedEventArgs e)
        {
            if (e.Url.StartsWith(redirectUri))
            {
                NameValueCollection coll = HttpUtility.ParseQueryString(e.Url.Split('?')[1]);
                authCode = coll.Get("code");
                if (authCode!=null)
                {
                    TokenResponse? res = await HttpCommunication.RequestAccessToken(authCode, redirectUri, Verifier);
                    if (res!=null)
                    {
                        TokenManager.AccessToken = res;
                        await SecureStorage.Default.SetAsync("oauth_token", JsonSerializer.Serialize(res));
                    }
                    await Shell.Current.GoToAsync("//AppPage");
                    return;
                }
                await Shell.Current.GoToAsync("//MainPage?failed=true");
            }
        }
    }
}
