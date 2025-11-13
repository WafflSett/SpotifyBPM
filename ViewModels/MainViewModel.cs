using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpotifyBPM.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyBPM.ViewModels
{
    [QueryProperty(nameof(Failed),"failed")]
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        bool failed = false;

        partial void OnFailedChanged(bool value) {
            if (value)
            {
                Shell.Current.DisplayAlert("Failed", "Authentication failed, please try again in a bit!", "Ok");
            }
        }

        [RelayCommand]
        public async Task StartAuth() {
            await Shell.Current.GoToAsync($"{nameof(AuthPage)}");
        }
    }
}
