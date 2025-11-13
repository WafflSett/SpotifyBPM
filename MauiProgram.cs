using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SpotifyBPM.Pages;
using SpotifyBPM.ViewModels;

namespace SpotifyBPM
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()

                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<AuthPage>();
            builder.Services.AddSingleton<AuthViewModel>();
            builder.Services.AddSingleton<AppPage>();
            builder.Services.AddSingleton<AppViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
