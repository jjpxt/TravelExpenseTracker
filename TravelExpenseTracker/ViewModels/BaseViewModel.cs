using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TravelExpenseTracker.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isBusy;

    protected async Task MakeApiCall(Func<Task> apiCall)
    {
		try
		{
			_isBusy = true;
			await apiCall.Invoke();
		}
		catch (Exception ex)
		{
			await ErrorAlertAsync(ex.Message);
        }
		finally
		{
			_isBusy = false;
		}
    }

	protected async Task ErrorAlertAsync(string message) =>
			await Shell.Current.DisplayAlert("Error", message, "Ok");

	protected async Task ToastAsync(string message) =>
		await Toast.Make(message).Show();
}

