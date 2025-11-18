using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpotifyBPM.Models;
using SpotifyBPM.Services;

namespace SpotifyBPM.ViewModels
{
    public partial class AppViewModel : ObservableObject
    {
        [ObservableProperty]
        TokenResponse myToken = TokenManager.AccessToken;

        [ObservableProperty]
        SpotifyUser myUser;

        [RelayCommand]
        public void LogOutClicked()
        {
            Shell.Current.GoToAsync("//MainPage");
        }
    }
}
