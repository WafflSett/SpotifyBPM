using SpotifyBPM.Classes;
using SpotifyBPM.Managers;
using SpotifyBPM.ViewModels;

namespace SpotifyBPM.Pages;

public partial class AppPage : ContentPage
{
	public AppPage(AppViewModel vm)
	{

		InitializeComponent();
		this.BindingContext = vm;
        SpotifyUser user = Task.Run(async () => await getMyUser()).Result;
        vm.MyUser = user;
	}

    private static async Task<SpotifyUser> getMyUser()
    {
        SpotifyUser user = await HttpCommunication<SpotifyUser>.Get("https://api.spotify.com/v1/me");
        if (user != null)
        {
            return user;
        }
        else
        {
            await TokenManager.RefreshToken();
            user = await HttpCommunication<SpotifyUser>.Get("https://api.spotify.com/v1/me");
        }
        return user;
    }
}