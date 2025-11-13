using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using SpotifyBPM.Classes;

namespace SpotifyBPM.ViewModels
{
    public partial class AppViewModel : ObservableObject
    {
        [ObservableProperty]
        TokenResponse myToken = App.AccessToken;


    }
}
