using SpotifyBPM.Models;
using SpotifyBPM.Services;
using SpotifyBPM.ViewModels;

namespace SpotifyBPM.Pages;

public partial class AppPage : ContentPage
{
	public AppPage(AppViewModel vm)
	{

		InitializeComponent();
		this.BindingContext = vm;
	}

}