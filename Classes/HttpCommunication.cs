using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SpotifyBPM;
using SpotifyBPM.Classes;

namespace FlagGame.Classes
{
    public static class HttpCommunication
    {

        public async static Task<TokenResponse>? RequestAccessToken(string code, string redirectUri, string codeVerifier)
        {
            using var client = new HttpClient();
            var data = new[] {
                new KeyValuePair<string, string>("client_id", App.ClientId),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", redirectUri),
                new KeyValuePair<string, string>("code_verifier", codeVerifier),
                new KeyValuePair<string, string>("grant_type", "authorization_code")
            };
            var content = new FormUrlEncodedContent(data);
            using var response = await client.PostAsync($"https://accounts.spotify.com/api/token", content); // .ConfigureAwait(false)
            if (response.IsSuccessStatusCode)
            {
                string resultString = response.Content.ReadAsStringAsync().Result;
                TokenResponse res = JsonSerializer.Deserialize<TokenResponse>(resultString)!;
                res.recievedAt = DateTime.Now;
                return res;
            }
            return null;
        }

        public async static Task<TokenResponse>? RefreshAccessToken(string refreshToken)
        {
            using var client = new HttpClient();
            var data = new[] {
                new KeyValuePair<string, string>("client_id", App.ClientId),
                new KeyValuePair<string, string>("refresh_token", refreshToken),
                new KeyValuePair<string, string>("grant_type", "refresh_token")
            };
            var content = new FormUrlEncodedContent(data);
            using var response = await client.PostAsync($"https://accounts.spotify.com/api/token", content); // .ConfigureAwait(false)
            if (response.IsSuccessStatusCode)
            {
                string resultString = response.Content.ReadAsStringAsync().Result;
                TokenResponse res = JsonSerializer.Deserialize<TokenResponse>(resultString)!;
                res.recievedAt = DateTime.Now;
                return res;
            }
            return null;
        }
    }
}
