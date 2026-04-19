using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TravelExpenseTracker.Models;

namespace TravelExpenseTracker.ViewModels;

public partial class TripsViewModel : ObservableObject
{
    public ObservableCollection<TripModel> Trips { get; set; } = [];

    [RelayCommand]
    private void AddTripTemp()
    {
        Trips.Add(new TripModel("trip1.png", "Trip one", "Some where"));
        Trips.Add(new TripModel("trip1.png", "Trip two", "Some where"));
        Trips.Add(new TripModel("trip1.png", "Third", "Some where"));
        Trips.Add(new TripModel("trip1.png", "Trip 4", "Some where"));
    }
}
