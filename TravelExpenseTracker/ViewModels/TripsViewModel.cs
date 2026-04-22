using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TravelExpenseTracker.Models;
using TravelExpenseTracker.Pages;

namespace TravelExpenseTracker.ViewModels;

public partial class TripsViewModel : ObservableObject
{
    public ObservableCollection<TripModel> Trips { get; set; } = [];

    [RelayCommand]
    private void AddTripTemp()
    {
        Trips.Add(new TripModel(1, "trip1.png", "Trip one", "Some where"));
        Trips.Add(new TripModel(2, "trip1.png", "Trip two", "Some where"));
        Trips.Add(new TripModel(3, "trip1.png", "Third", "Some where"));
        Trips.Add(new TripModel(4, "trip1.png", "Trip 4", "Some where"));
    }

    [RelayCommand]
    private async Task GoToTripDetailsPageAsync(int tripId)
    {
        await Shell.Current.GoToAsync(nameof(TripDetailsPage));
    }
}
