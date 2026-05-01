using Microsoft.AspNetCore.Mvc;
using TravelExpenseTracker.Shared.Dtos;
using TravelExpenseTracker_API.Services;

namespace TravelExpenseTracker_API.Controllers;

[Route("api/trips/expenses")]
[ApiController]
public class ExpenseController : AppBaseController
{
    private readonly TripExpenseService _service;

    public ExpenseController(TripExpenseService service)
    {
        _service = service;
    }

    [HttpGet("categories")]
    public async Task<ExpenseCategoryDto[]> GetExpensesCategories() =>
        await _service.GetExpensesCategoriesAsync(UserId);

    [HttpPost("categories")]
    public async Task<ApiResult> SaveExpenseCategory(ExpenseCategoryDto dto) =>
        await _service.SaveExpenseCategoryAsync(dto, UserId);

    [HttpPost("of-trip/{tripId:int}/save")]
    public async Task<ApiResult> SaveTripExpense(int tripId, ExpenseDto dto) =>
        await _service.SaveTripExpenseAsync(tripId, dto, UserId);

    [HttpGet("of-trip/{tripId:int}")]
    public async Task<ExpenseListDto[]> GetTripExpenses(int tripId) =>
        await _service.GetTripExpensesAsync(tripId, UserId);
}
