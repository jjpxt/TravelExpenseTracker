using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TravelExpenseTracker.Models;

namespace TravelExpenseTracker.ViewModels;

public partial class ExpensesCategoriesViewModel : ObservableObject
{
    public ObservableCollection<ExpenseCategoryModel> Categories { get; set; } = [];

    public ExpensesCategoriesViewModel()
    {
        ExpenseCategoryModel[] tempExpenseCategories = [
            new (1, "Tickets"),
            new (2, "Shopping"),
            new (3, "Food"),
            new (4, "Fuel"),
            new (5, "Other"),
            ];

        foreach (var item in tempExpenseCategories)
        {
            Categories.Add(item);
        }

    }

    [RelayCommand]
    private async Task AddCategoriesAsync()
    {
        var newCategoryName = await Shell.Current.DisplayPromptAsync("Expense category name", "Enter new expense Category", "Add");
        if (!string.IsNullOrEmpty(newCategoryName))
        {
            var newCategory = new ExpenseCategoryModel(Categories.Count + 1, newCategoryName);
            Categories.Add(newCategory);
            return;
        }
        await Toast.Make("Invalid expense category name").Show();
    }
}

