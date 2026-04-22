using TravelExpenseTracker.ViewModels;

namespace TravelExpenseTracker.Pages;

public partial class ExpensesCategoriesPage : ContentPage
{
	public ExpensesCategoriesPage(ExpensesCategoriesViewModel expensesCategoriesViewModel)
	{
		InitializeComponent();
		BindingContext = expensesCategoriesViewModel;
	}
}