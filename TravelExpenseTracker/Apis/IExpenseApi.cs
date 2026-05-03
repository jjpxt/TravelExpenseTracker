using Refit;
using TravelExpenseTracker.Shared.Dtos;

namespace TravelExpenseTracker.Apis;

[Headers("Authorization: Bearer ")]
public interface IExpenseApi
{
    [Get("/api/trips/expenses/categories")]
    Task<ExpenseCategoryDto[]> GetExpensesCategories();

    [Post("/api/trips/expenses/categories")]
    Task<ApiResult> SaveExpenseCategory(ExpenseCategoryDto dto);

    [Post("/api/trips/expenses/of-trip/{tripId}/save")]
    Task<ApiResult> SaveTripExpense(int tripId, ExpenseDto dto);

    [Get("/api/trips/expenses/of-trip/{tripId}")]
    Task<ExpenseListDto[]> GetTripExpenses(int tripId);

}