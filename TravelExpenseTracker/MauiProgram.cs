using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Refit;
using TravelExpenseTracker.Apis;
using TravelExpenseTracker.Pages;
using TravelExpenseTracker.Services;
using TravelExpenseTracker.ViewModels;

namespace TravelExpenseTracker;

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
        builder.Services.AddTransient<ExpensesCategoriesViewModel>().AddTransient<ExpensesCategoriesPage>();
        builder.Services.AddTransient<TripDetailsViewModel>().AddTransient<TripDetailsPage>();
        builder.Services.AddTransient<SaveExpenseViewModel>().AddTransient<SaveExpensePage>();

        builder.Services.AddSingleton<AuthService>();

        ConfigureRefit(builder.Services);

        return builder.Build();
    }
private static void ConfigureRefit(IServiceCollection services)
    {
        const string ApiBaseUrl = "https://7k864sdx-7205.brs.devtunnels.ms";

        services.AddRefitClient<IAuthApi>()
            .ConfigureHttpClient(SetHttpClient);

        services.AddRefitClient<IExpenseApi>(GetRefitSettings)
            .ConfigureHttpClient(SetHttpClient);

        services.AddRefitClient<IProfileApi>(GetRefitSettings)
            .ConfigureHttpClient(SetHttpClient);

        services.AddRefitClient<ITripsApi>(GetRefitSettings)
            .ConfigureHttpClient(SetHttpClient);

        static RefitSettings GetRefitSettings(IServiceProvider sp)
        {
            var authService = sp.GetRequiredService<AuthService>();

            return new RefitSettings
            {
                AuthorizationHeaderValueGetter = (_, __) => Task.FromResult(authService.Token ?? "")
            };
        }

        static void SetHttpClient(HttpClient httpClient) => httpClient.BaseAddress = new Uri(ApiBaseUrl);
    }
}
