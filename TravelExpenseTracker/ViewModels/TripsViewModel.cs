using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TravelExpenseTracker.Apis;
using TravelExpenseTracker.Models;
using TravelExpenseTracker.Pages;
using TravelExpenseTracker.Shared.Dtos;

namespace TravelExpenseTracker.ViewModels;

public partial class TripsViewModel : BaseViewModel
{
    private readonly ITripsApi _tripsApi;
    public TripsViewModel(ITripsApi tripsApi)
    {
        _tripsApi = tripsApi;
    }

    [RelayCommand]
    private async Task AddTrip()
    {
        await Shell.Current.GoToAsync($"//{nameof(SaveTripPage)}");
    }

    [ObservableProperty]
    private TripListDto[] _trips = [];

    public async Task FetchTripsAsync()
    {
        await MakeApiCall(async () =>
        {
            Trips = await _tripsApi.GetUserTrips(1000);
        });
    }

    [RelayCommand]
    private async Task GoToTripDetailsPageAsync(int tripId)
    {
        var parameter = new Dictionary<string, object>
        {
            [nameof(TripDetailsViewModel.TripId)] = tripId
        };

        await Shell.Current.GoToAsync(nameof(TripDetailsPage), parameter);
    }
}
