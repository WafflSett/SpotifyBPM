using Org.Apache.Http.Authentication;
using SpotifyBPM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpotifyBPM.Services
{
    public static class TokenManager
    {
        public static TokenResponse? AccessToken { get; set; }
        
        // gets token using auth code and saves it to secure storage
        public static async Task<TokenResponse?> GetToken(string authCode, string redirectUri, string verifier)
        {
            TokenResponse? res = await HttpCommunication.RequestAccessToken(authCode, redirectUri, verifier);
            if (res != null)
            {
                AccessToken = res;
                await SecureStorage.Default.SetAsync("oauth_token", JsonSerializer.Serialize(res));
            }
            return res;
        }
        
        // loads token from secure storage if it exists
        public static void LoadToken() {
            string? storageResult = Task.Run(async () => await SecureStorage.Default.GetAsync("oauth_token")).Result;
            if (storageResult != null)
            {
                TokenResponse? oauthToken = JsonSerializer.Deserialize<TokenResponse?>(storageResult);
                AccessToken = oauthToken;
            }
        }
        
        // removes token from secure storage
        public static void LogOut() {
            SecureStorage.Default.Remove("oauth_token");
            AccessToken = null;
            UserManager.CurrentUser = null;
        }
        
        // refreshes access token using refresh token
        public static async Task RefreshToken() {
            AccessToken = await HttpCommunication.RefreshAccessToken(AccessToken.refresh_token);
        }
        
        // checks if token exists and is valid, refreshes if expired
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
