using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SpotifyBPM.Classes;

namespace FlagGame.Classes
{
    public static class HttpCommunication
    {

        public async static Task<TokenResponse>? RequestAccessToken(string code, string redirectUri, string clientId, string codeVerifier)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
            Dictionary<string,string> dict = new Dictionary<string, string>();
            var request = new HttpRequestMessage(HttpMethod.Post, (new UriBuilder($"https://accounts.spotify.com/api/token?grant_type=authorization_code&code={code}&redirect_uri={redirectUri}&client_id={clientId}&code_verifier={codeVerifier}")).Uri.AbsoluteUri) { Content = new FormUrlEncodedContent(dict)};
            using var response = await client.SendAsync(request); // .ConfigureAwait(false)
            if (response.IsSuccessStatusCode)
            {
                string resultString = response.Content.ReadAsStringAsync().Result;
                TokenResponse res = JsonSerializer.Deserialize<TokenResponse>(resultString);
                return res;
            }
            return null;
        }
    }
}
