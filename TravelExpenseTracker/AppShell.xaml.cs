using TravelExpenseTracker.Pages;

namespace TravelExpenseTracker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(ExpensesCategoriesPage), typeof(ExpensesCategoriesPage));
            Routing.RegisterRoute(nameof(TripDetailsPage), typeof(TripDetailsPage));
        }
    }
}
