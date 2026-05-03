using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TravelExpenseTracker.Apis;
using TravelExpenseTracker.Models;
using TravelExpenseTracker.Pages;
using TravelExpenseTracker.Shared.Dtos;

namespace TravelExpenseTracker.ViewModels;

[QueryProperty(nameof(TripId), nameof(TripId))]
[QueryProperty(nameof(ExpenseSaved), nameof(ExpenseSaved))]
public partial class TripDetailsViewModel : BaseViewModel
{
    private readonly ITripsApi _tripsApi;
    private readonly IExpenseApi _expenseApi;
    public TripDetailsViewModel(ITripsApi tripsApi, IExpenseApi expenseApi)
    {
        _tripsApi = tripsApi;
        _expenseApi = expenseApi;
    }

    [ObservableProperty]
    private TripListDto _tripInfo = TripListDto.Empty();

    [ObservableProperty, NotifyPropertyChangedFor(nameof(TotalExpenses))]
    private ExpenseListDto[] _expenses = [];

    [ObservableProperty]
    private int _tripId;

    async partial void OnTripIdChanged(int value)
    {
        await MakeApiCall(async () =>
        {
            var result = await _tripsApi.GetTripDetails(TripId);
            if (!result.IsSuccess)
            {
                await ErrorAlertAsync(result.Error);
                return;
            }

            (TripInfo, Expenses) = result.Data;
        });
    }

    public decimal TotalExpenses => Expenses.Sum(e => e.Amount);

    [RelayCommand]
    private async Task AddExpenseAsync()
    {
        var parameter = new Dictionary<string, object>
        {
            [nameof(SaveExpenseViewModel.TripId)] = TripId
        };
        await Shell.Current.GoToAsync(nameof(SaveExpensePage), parameter);
    }

    [ObservableProperty]
    private bool _expenseSaved;

    async partial void OnExpenseSavedChanging(bool newValue)
    {
        if (newValue)
        {
            await MakeApiCall(async () =>
             {
                   Expenses = await _expenseApi.GetTripExpenses(TripId);
                   ExpenseSaved = false;
              });
        }
    }
}
