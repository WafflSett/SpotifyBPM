using SpotifyBPM.ViewModels;

namespace SpotifyBPM.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel mv)
        {
            InitializeComponent();
            this.BindingContext = mv;
        }

        
    }

}
