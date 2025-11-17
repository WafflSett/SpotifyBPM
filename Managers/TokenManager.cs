using SpotifyBPM.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpotifyBPM.Managers
{
    public static class TokenManager
    {
        public static TokenResponse? AccessToken { get; set; }

        public static void LoadToken() {
            string? storageResult = Task.Run(async () => await SecureStorage.Default.GetAsync("oauth_token")).Result;
            if (storageResult != null)
            {
                TokenResponse? oauthToken = JsonSerializer.Deserialize<TokenResponse?>(storageResult);
                AccessToken = oauthToken;
            }
        }

        public static void GetToken() { 
            
        }

        public static async Task LogOut() {
            await SecureStorage.Default.SetAsync("oauth_token","");
            AccessToken = null;
        }

        public static async Task RefreshToken() {
            AccessToken = await HttpCommunication.RefreshAccessToken(AccessToken.refresh_token);
        }
        public static bool CheckToken()
        {
            if (AccessToken != null)
            {
                // check if token expired yet
                if (AccessToken.recievedAt.AddSeconds(AccessToken.expires_in) < DateTime.Now)
                {
                    //refresh token
                    Task.Run(async()=>await RefreshToken());
                    if (AccessToken != null)
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
    }
}
