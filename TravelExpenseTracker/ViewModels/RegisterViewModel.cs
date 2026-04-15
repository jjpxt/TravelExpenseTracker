using CommunityToolkit.Mvvm.ComponentModel;

namespace TravelExpenseTracker.ViewModels;

public partial class RegisterViewModel : ObservableObject
{
    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _password;

}
