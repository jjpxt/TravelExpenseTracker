using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TravelExpenseTracker.Pages;

namespace TravelExpenseTracker.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _password;

    [RelayCommand]
    private async Task NavigateToRegisterAsync() =>
        await Shell.Current.GoToAsync(nameof(RegisterPage));
}
