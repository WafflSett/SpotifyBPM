using SpotifyBPM.Classes;
using SpotifyBPM.ViewModels;

namespace SpotifyBPM.Pages;

public partial class AuthPage : ContentPage
{
	public AuthPage(AuthViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
        (string code_challange, string verifier) codes = Pkce.Generate();
		vm.Verifier = codes.verifier;
		vm.CodeChallange = codes.code_challange;
		vm.StartBrowser();
    }
}