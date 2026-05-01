using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TravelExpenseTracker.Apis;
using TravelExpenseTracker.Pages;
using TravelExpenseTracker.Services;
using TravelExpenseTracker.Shared.Dtos;

namespace TravelExpenseTracker.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    private readonly IAuthApi _authApi;
    private readonly AuthService _authService;

    public LoginViewModel(IAuthApi authApi, AuthService authService)
    {
        _authApi = authApi;
        _authService = authService;
    }

    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _password;

    [RelayCommand]
    private async Task NavigateToRegisterAsync() =>
        await Shell.Current.GoToAsync(nameof(RegisterPage));

    [RelayCommand]
    private async Task LoginAsync()
    {
        if(string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
        {
            await ErrorAlertAsync("Insert all fields");
            return;
        }

        var logginDto = new LoginDto
        {
            Email = Email,
            Password = Password
        };

        await MakeApiCall(async() =>
        {
            var result = await _authApi.LoginAsync(logginDto);
            if (!result.IsSuccess)
            {
                await ErrorAlertAsync(result.Error);
                return;
            }

            var jwtToken = result.Data;
            _authService.SetToken(jwtToken);
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }); 

    }
}
