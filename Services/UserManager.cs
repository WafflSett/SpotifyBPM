using SpotifyBPM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyBPM.Services
{
    public class UserManager
    {
        public static SpotifyUser? CurrentUser { get; set; }

        // Retrieves the current user's Spotify profile information
        public static async Task GetCurrentUser()
        {
            if (TokenManager.CheckToken())
            {   
                SpotifyUser user = await HttpCommunication<SpotifyUser>.Get("https://api.spotify.com/v1/me");
                CurrentUser = user;
            }
        }
    }
}
