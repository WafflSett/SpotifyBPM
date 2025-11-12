using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SpotifyBPM.Classes;

namespace FlagGame.Classes
{
    public static class HttpCommunication<T> where T : class
    {
        public async static Task<T?> Get(string url) 
        {
            using var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            using var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode) { 
                string resultString = response.Content.ReadAsStringAsync().Result;
                T? root = JsonSerializer.Deserialize<T>(resultString);
                return root;
            }
            return null;
        }

        public async static Task<TokenResponse>? RequestAccessToken()
        {
            using var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token");
            using var response = await client.SendAsync(request).ConfigureAwait(false);
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
