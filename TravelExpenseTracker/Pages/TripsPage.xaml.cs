using System.Threading.Tasks;
using TravelExpenseTracker.ViewModels;

namespace TravelExpenseTracker.Pages;

public partial class TripsPage : ContentPage
{
    private readonly TripsViewModel viewModel;

    public TripsPage(TripsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        this.viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.FetchTripsAsync();
    }
}