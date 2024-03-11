using CommunityToolkit.Maui;
using Mde.PlatformIntegration.Domain.Services;
using Mde.PlatformIntegration.Pages;
using Mde.PlatformIntegration.Platforms.Services;
using Mde.PlatformIntegration.ViewModels;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using UraniumUI;


namespace Mde.PlatformIntegration
{
    public static class MauiProgram
    {

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFontAwesomeIconFonts();
                });

            //register views and viewmodels
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<AudioPlayerPage>();
            builder.Services.AddTransient<AudioPlayerViewModel>();

            //register domain services
            builder.Services.AddTransient<IMusicService, BundledMusicService>();
            builder.Services.AddTransient<IDispatcherTimer>((services) => Application.Current.Dispatcher.CreateTimer());

            //register platform specific services
            builder.Services.AddSingleton<INativeAuthentication, NativeAuthentication>();
            builder.Services.AddSingleton<IAudioManager>(new AudioManager());

            //register shell routes
            Routing.RegisterRoute("login", typeof(LoginPage));
            Routing.RegisterRoute("audioplayer", typeof(AudioPlayerPage));

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
