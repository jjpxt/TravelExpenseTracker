using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TravelExpenseTracker.Apis;
using TravelExpenseTracker.Models;
using TravelExpenseTracker.Shared.Dtos;

namespace TravelExpenseTracker.ViewModels;

public partial class ExpensesCategoriesViewModel : BaseViewModel
{
    private readonly IExpenseApi _expenseApi;

    public ObservableCollection<ExpenseCategoryDto> Categories { get; set; } = [];

    public ExpensesCategoriesViewModel(IExpenseApi expenseApi)
    {
        _expenseApi = expenseApi;
    }

    public async Task FetchCategoryAsync()
    {
        Categories.Clear();
        await MakeApiCall(async () =>
        {
            var categories = await _expenseApi.GetExpensesCategories();
            foreach(var category in categories)
            {
                Categories.Add(category);
            }
        });
    }

    [RelayCommand]
    private async Task AddCategoriesAsync()
    {
        var newCategoryName = await Shell.Current.DisplayPromptAsync("Expense category name", "Enter new expense Category", "Add");
        if (!string.IsNullOrEmpty(newCategoryName))
        {
            await MakeApiCall(async () =>
            {
                var dto = new ExpenseCategoryDto
                {
                    Name = newCategoryName
                };
                var result = await _expenseApi.SaveExpenseCategory(dto);
                if (!result.IsSuccess)
                {
                    await ErrorAlertAsync(result.Error);
                    return;
                }

                Categories.Add(dto);
                await ToastAsync("Category added");
            });
            return;
        }
        await ToastAsync("Invalid expense category name");
    }
}

