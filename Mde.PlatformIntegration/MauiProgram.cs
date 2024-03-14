using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.Messaging;
using Mde.PlatformIntegration.Domain.Services;
using Mde.PlatformIntegration.Pages;
using Mde.PlatformIntegration.Platforms.Services;
using Mde.PlatformIntegration.ViewModels;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using SkiaSharp.Views.Maui.Controls.Hosting;
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
                .UseMauiCommunityToolkit()  //register toolkit services
                .UseSkiaSharp()             //register skia handlers

                .UseUraniumUI()             //register custom UI framework
                .UseUraniumUIMaterial()     //register custom UI framework material theme

                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("PixelifySans-Regular.ttf", "PixelifySans");
                    fonts.AddFontAwesomeIconFonts();
                });

            //register views, viewmodels and routes
            builder.Services.AddTransientWithShellRoute<MainPage, MainViewModel>("main");
            builder.Services.AddTransientWithShellRoute<ProfilePage, ProfileViewModel>("profile");
            builder.Services.AddTransientWithShellRoute<AudioPlayerPage, AudioPlayerViewModel>("audioplayer");
            builder.Services.AddTransientWithShellRoute<RecordAudioPage, RecordAudioViewModel>("audiorecorder");

            //register domain services
            builder.Services.AddTransient<IMusicService, BundledMusicService>();
            builder.Services.AddTransient<IProfileService, SecureProfileService>();
            builder.Services.AddTransient<IDialogService, DialogService>();
            builder.Services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);
            builder.Services.AddSingleton<ISecureStorage>(SecureStorage.Default);
            builder.Services.AddTransient<IDispatcherTimer>((services) => Application.Current.Dispatcher.CreateTimer());

            //register platform specific services
            builder.Services.AddSingleton<INativeAuthentication, NativeAuthentication>();
            builder.Services.AddSingleton<IAudioManager>(new AudioManager());


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
