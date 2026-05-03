using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TravelExpenseTracker.Apis;
using TravelExpenseTracker.Models;
using TravelExpenseTracker.Pages;
using TravelExpenseTracker.Shared;

namespace TravelExpenseTracker.ViewModels;


public partial class SaveTripViewModel : BaseViewModel
{
    private readonly ITripsApi _tripsApi;

    public SaveTripViewModel(ITripsApi tripsApi)
    {
        _tripsApi = tripsApi;
        FetchCategoriesAsync(); 
    }


    //async partial void OnTripIdChanged(int value)
    //{
    //    if( value > 0)
    //    {
    //        await MakeApiCall(async () =>
    //        {
    //            var result = await _tripsApi.GetTripDetails(value, includeExpenses: false);
    //            if (!result.IsSuccess)
    //            {
    //                await ErrorAlertAsync(result.Error);
    //                return;
    //            }
    //            var tripInfo = result.Data;
    //            Model = SaveTripModel.FromDto(tripInfo.TripInfo);

    //        });
    //    }
    //}

    [ObservableProperty]
    private CategoryModel[] _categories = [];

    public async Task FetchCategoriesAsync()
    {
        await MakeApiCall(async () =>
        {
            var categoriesFromApi = await _tripsApi.GetCategories();
            Categories = categoriesFromApi.Select(c => new CategoryModel
            {
                Name = c.Name,
                Id = c.Id,
                Image = c.Image,
                IsSelected = false
            }).ToArray();
        });
    }

    public string[] Statuses { get; set; } =
    [
        nameof(TripStatus.Planned),
        nameof(TripStatus.Ongoing),
        nameof(TripStatus.Completed),
        nameof(TripStatus.Cancelled),
    ];

    //public SaveTripModel Model { get; set; } = new();
    [ObservableProperty]
    private SaveTripModel _model = new();

    [RelayCommand]
    private void SetSelectedCategory(CategoryModel category)
    {
        if (category.IsSelected)
            return;

        var existingSelectedCategory = Categories.FirstOrDefault(c => c.IsSelected);

        if (existingSelectedCategory is not null)
            existingSelectedCategory.IsSelected = false;

        category.IsSelected = true;
        Model.CategoryId = category.Id;
    }

    [RelayCommand]
    private async Task SaveTripAsync()
    {
        var (isValid, error) = Model.Validate();
        if (!isValid)
        {
            await ErrorAlertAsync(error);
            return;
        }

        await MakeApiCall(async () =>
        {
            var result = await _tripsApi.SaveTrip(Model.ToDto());
            if (!result.IsSuccess)
            {
                await ErrorAlertAsync(result.Error);
                return;
            }

            Model = new();
            await ToastAsync("Trip saved");
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        });
    }
}
