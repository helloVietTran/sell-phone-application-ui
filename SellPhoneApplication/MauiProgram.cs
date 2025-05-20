using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SellPhoneApplication.View;

namespace SellPhoneApplication
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.UseMauiCommunityToolkit();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<RegisterViewModel>();
            builder.Services.AddSingleton < PhonesViewModel>();
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<RegisterPage>();
            builder.Services.AddSingleton<PhonesPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
