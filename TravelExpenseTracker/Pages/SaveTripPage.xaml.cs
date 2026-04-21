using TravelExpenseTracker.ViewModels;

namespace TravelExpenseTracker.Pages;

public partial class SaveTripPage : ContentPage
{
	public SaveTripPage(SaveTripViewModel saveTripViewModel)
	{
		InitializeComponent();
		BindingContext = saveTripViewModel;
	}
}