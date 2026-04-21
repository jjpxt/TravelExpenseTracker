using CommunityToolkit.Mvvm.ComponentModel;

namespace TravelExpenseTracker.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty, NotifyPropertyChangedFor(nameof(Initial))]
    private string _name = "Euzinho da Silva";

    public string Initial => Name[0].ToString().ToUpper();
}
