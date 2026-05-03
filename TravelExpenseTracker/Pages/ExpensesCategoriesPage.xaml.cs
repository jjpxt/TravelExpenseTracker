using System.Threading.Tasks;
using TravelExpenseTracker.ViewModels;

namespace TravelExpenseTracker.Pages;

public partial class ExpensesCategoriesPage : ContentPage
{
    private readonly ExpensesCategoriesViewModel _expensesCategoriesViewModel;

    public ExpensesCategoriesPage(ExpensesCategoriesViewModel expensesCategoriesViewModel)
	{
		InitializeComponent();
		BindingContext = expensesCategoriesViewModel;
        _expensesCategoriesViewModel = expensesCategoriesViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _expensesCategoriesViewModel.FetchCategoryAsync();
    }
}