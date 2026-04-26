using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TravelExpenseTracker.Models;
using TravelExpenseTracker.Pages;

namespace TravelExpenseTracker.ViewModels;

public partial class TripDetailsViewModel : ObservableObject
{
    public TripDetailModel TripInfo { get; set; } = new TripDetailModel(
        "category_beach.png", "A Beach trip", "Some place", "beach", "Planned",
        DateTime.Now.AddDays(10), DateTime.Now.AddDays(20));

    public ObservableCollection<ExpenseModel> Expenses { get; set; } = [];

    [ObservableProperty]
    private decimal _totalExpenses;

    [RelayCommand]
    private async Task AddExpenseTempAsync()
    {
        Expenses.Add(new ExpenseModel(1, "Flight tickets", "Tickets", 1500, DateTime.Today));
        Expenses.Add(new ExpenseModel(2, "Clothes, Shoes and cosmetic", "Shopping", 100, DateTime.Today));
        Expenses.Add(new ExpenseModel(3, "Breakfast", "Food", 500, DateTime.Now.AddDays(2)));

        TotalExpenses = Expenses.Sum(e => e.Amount);

        await Shell.Current.GoToAsync(nameof(SaveExpensePage));
    }
}
