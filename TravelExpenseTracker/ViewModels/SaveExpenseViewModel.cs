using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TravelExpenseTracker.Apis;
using TravelExpenseTracker.Models;
using TravelExpenseTracker.Shared.Dtos;

namespace TravelExpenseTracker.ViewModels;

[QueryProperty(nameof(TripId), nameof(TripId))]
public partial class SaveExpenseViewModel : BaseViewModel
{
    private readonly IExpenseApi _expenseApi;

    public SaveExpenseViewModel(IExpenseApi expenseApi)
    {
        _expenseApi = expenseApi;
    }

    [ObservableProperty]
    private int _tripId;

    [ObservableProperty]
    private SaveExpenseModel _model = new();

    [ObservableProperty]
    private string[] _categoryNames = [];

    private ExpenseCategoryDto[] _categories = [];
    public async Task FetchCategoriesAsync()
    {
        await MakeApiCall(async () =>
        {
            _categories = await _expenseApi.GetExpensesCategories();
            CategoryNames = [.. _categories.Select(c => c.Name)];
        });
    }

    [RelayCommand]
    private async Task GoBackAsync() => await Shell.Current.GoToAsync("..");

    [RelayCommand]
    private async Task SaveExpenseAsync()
    {
        Model.CategoryId = _categories.FirstOrDefault(c => c.Name == Model.SelectedCategoryName)?.Id ?? 0;

        var (isValid, error) = Model.Validate();
        if (!isValid)
        {
            await ErrorAlertAsync(error);
            return;
        }

        await MakeApiCall(async () =>
        {
            var result = await _expenseApi.SaveTripExpense(TripId, Model.ToDto());
            if (!result.IsSuccess)
            {
                await ErrorAlertAsync(result.Error);
                return;
            }

            await ToastAsync("Expense saved");

            var parameter = new Dictionary<string, object>
            {
                [nameof(TripDetailsViewModel.ExpenseSaved)] = true
            };
            await Shell.Current.GoToAsync("..", parameter);
        });
    }
}
