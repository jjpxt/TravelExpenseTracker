using CommunityToolkit.Mvvm.ComponentModel;

namespace TravelExpenseTracker.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _password;
}
