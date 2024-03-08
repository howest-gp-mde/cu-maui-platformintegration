using CommunityToolkit.Maui;
using Mde.PlatformIntegration.Domain.Services;
using Mde.PlatformIntegration.Pages;
using Mde.PlatformIntegration.Platforms.Services;
using Mde.PlatformIntegration.ViewModels;
using Microsoft.Extensions.Logging;
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

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();

            builder.Services.AddSingleton<INativeAuthentication, NativeAuthentication>();

            Routing.RegisterRoute("login", typeof(LoginPage));

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
