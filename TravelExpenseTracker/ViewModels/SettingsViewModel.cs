using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TravelExpenseTracker.Apis;
using TravelExpenseTracker.Pages;
using TravelExpenseTracker.Services;
using TravelExpenseTracker.Shared.Dtos;

namespace TravelExpenseTracker.ViewModels;

public partial class SettingsViewModel : BaseViewModel
{
    private readonly IProfileApi _profileApi;
    private readonly AuthService _authService;

    public SettingsViewModel(IProfileApi profileApi, AuthService authService)
    {
        _profileApi = profileApi;
        _authService = authService;
        Name = authService.User.Name;
    }

    [ObservableProperty, NotifyPropertyChangedFor(nameof(Initial))]
    private string _name = "";

    public string Initial => !string.IsNullOrWhiteSpace(Name) ? Name[0].ToString().ToUpper() : "";

    [RelayCommand]
    private async Task GoToExpenseCategoriesAsync() =>
        await Shell.Current.GoToAsync(nameof(ExpensesCategoriesPage));

    [RelayCommand]
    private async Task UpdateNameAsync()
    {
        var newName = await Shell.Current.DisplayPromptAsync("Update Name", "Please enter name update", "Change Name");
        if (string.IsNullOrWhiteSpace(newName))
        {
            await ToastAsync("Invalid name");
            return;
        }

        await MakeApiCall(async () =>
        {
            var result = await _profileApi.UpdateName(new SingleValueDto<string>(newName));
            if (!result.IsSuccess)
            {
                await ErrorAlertAsync(result.Error);
                return;
            }

            var newJwtToken = result.Data;
            _authService.SetToken(newJwtToken);
            Name = _authService.User.Name;
            await ToastAsync("Name updated successfully");
        });
    }

    [RelayCommand]
    private async Task ChangePasswordAsync()
    {
        var newPassword = await Shell.Current.DisplayPromptAsync("Change Password", "Please enter new password", "Change Password");
        if (string.IsNullOrWhiteSpace(newPassword))
        {
            await ToastAsync("Invalid password");
            return;
        }

        await MakeApiCall(async () =>
        {
            var result = await _profileApi.ChangePassword(new SingleValueDto<string>(newPassword));
            if (!result.IsSuccess)
            {
                await ErrorAlertAsync(result.Error);
                return;
            }

            await ToastAsync("Password change successfully");
            await LogoutAsync();
        });
    }

    [RelayCommand]
    private async Task LogoutAsync()
    {
        _authService.RemoveToken();
        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }
}
