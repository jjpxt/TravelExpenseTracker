using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TravelExpenseTracker.Pages;
using TravelExpenseTracker.ViewModels;

namespace TravelExpenseTracker
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
                })
                .UseMauiCommunityToolkit();

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<LoginViewModel>().AddTransient<LoginPage>();
            builder.Services.AddTransient<RegisterViewModel>().AddTransient<RegisterPage>();

            builder.Services.AddSingleton<HomeViewModel>().AddSingleton<MainPage>();

            builder.Services.AddTransient<TripsViewModel>().AddTransient<TripsPage>();
            builder.Services.AddTransient<SettingsViewModel>().AddTransient<SettingsPage>();
            builder.Services.AddTransient<SaveTripViewModel>().AddTransient<SaveTripPage>();

            return builder.Build();
        }
    }
}
