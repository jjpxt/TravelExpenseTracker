using Microsoft.EntityFrameworkCore;
using TravelExpenseTracker.Shared.Dtos;
using TravelExpenseTracker_API.Data;
using TravelExpenseTracker_API.Data.Entities;

namespace TravelExpenseTracker_API.Services;

public class TripExpenseService
{
    private readonly DataContext _context;

    public TripExpenseService(DataContext context)
    {
        _context = context;
    }

    public async Task<ExpenseCategoryDto[]> GetExpensesCategoriesAsync(int userId) =>
        await _context.ExpenseCategories.AsNoTracking().Where(e => e.UserId == null || e.UserId == userId)
        .Select(c => new ExpenseCategoryDto
        {
            Id = c.Id,
            UserId = c.UserId,
            Name = c.Name
        })
        .ToArrayAsync();

    public async Task<ApiResult> SaveExpenseCategoryAsync(ExpenseCategoryDto dto, int userId)
    {
        if (await _context.ExpenseCategories.AsNoTracking().AnyAsync(e => e.Name == dto.Name && e.UserId == userId || e.UserId == null))
        {
            return ApiResult.Fail("Category already exists");
        }

        var category = new ExpenseCategory
        {
            Name = dto.Name,
            UserId = userId
        };

        _context.ExpenseCategories.Add(category);
        await _context.SaveChangesAsync();
        return ApiResult.Success();
    }

    public async Task<ApiResult> SaveTripExpenseAsync(int tripId, ExpenseDto dto, int userId)
    {
        var trip = await _context.Trips.FindAsync(tripId);

        if (trip is null)
            return ApiResult.Fail("Invalid request");

        if (trip.UserId != userId)
            return ApiResult.Fail("You are not authorized");

        try
        {
            var expense = new TripExpense
            {
                TripId = tripId,
                Title = dto.Title,
                Amount = dto.Amount,
                ExpenseCategoryId = dto.CategoryId,
                SpentOn = dto.SpentOn
            };

            _context.TripExpenses.Add(expense);
            await _context.SaveChangesAsync();
            return ApiResult.Success();
        }
        catch (Exception ex)
        {
            return ApiResult.Fail(ex.Message);
        }
    }

    public async Task<ExpenseListDto[]> GetTripExpensesAsync(int tripId, int userId) =>
        await _context.Trips.AsNoTracking()
        .Where(t => t.Id == tripId && t.UserId == userId)
        .SelectMany(t => t.Expenses)
        .Select(e => new ExpenseListDto(e.Id, e.Title, e.ExpenseCategory.Name, e.Amount, e.SpentOn))
        .ToArrayAsync();
}


