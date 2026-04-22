using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TravelExpenseTracker.Pages;

namespace TravelExpenseTracker.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty, NotifyPropertyChangedFor(nameof(Initial))]
    private string _name = "Euzinho da Silva";

    public string Initial => Name[0].ToString().ToUpper();

    [RelayCommand]
    private async Task GoToExpenseCategoriesAsync() =>
        await Shell.Current.GoToAsync(nameof(ExpensesCategoriesPage));
}
