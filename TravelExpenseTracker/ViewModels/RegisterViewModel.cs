using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TravelExpenseTracker.Apis;
using TravelExpenseTracker.Shared.Dtos;

namespace TravelExpenseTracker.ViewModels;

public partial class RegisterViewModel : BaseViewModel
{
    private readonly IAuthApi _authApi;
    public RegisterViewModel(IAuthApi authApi)
    {
        _authApi = authApi;
    }

    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _password;

    [RelayCommand]
    private async Task NavigateBackAsync() =>
    await Shell.Current.GoToAsync("..");

    [RelayCommand]
    private async Task RegisterAsync()
    {
        if(string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
        {
            await ErrorAlertAsync("Insert all fields");
            return;
        }

        var registerDto = new RegisterDto
        {
            Email = Email,
            Name = Name,
            Password = Password
        };

        await MakeApiCall(async () =>
        {
            var result = await _authApi.RegisterAsync(registerDto);
            if (!result.IsSuccess)
            {
                await ErrorAlertAsync(result.Error);
                return;
            }

            await ToastAsync("Succesfully registered");
            await NavigateBackAsync();
        });
    }
}
