using Microsoft.AspNetCore.Mvc;
using TravelExpenseTracker.Shared.Dtos;
using TravelExpenseTracker_API.Services;

namespace TravelExpenseTracker_API.Controllers;

[Route("api/trips")]
[ApiController]
public class TripsController : AppBaseController
{
    private readonly TripsService _tripsService;

    public TripsController(TripsService tripsService)
    {
        _tripsService = tripsService;
    }

    [HttpGet("categories")]
    public async Task<CategoryDto[]> GetCategories() =>
        await _tripsService.GetCategoriesAsync();

    [HttpPost("")]
    public async Task<ApiResult> SaveTrip(SaveTripDto dto) =>
        await _tripsService.SaveTripResultAsync(dto, UserId);

    [HttpGet("")]
        public async Task<TripListDto[]> GetUserTrips(int count = 100) =>
        await _tripsService.GetUserTripsAsync(UserId, count);

    [HttpGet("{tripId:int}")]
    public async Task<ApiResult<TripDetailsDto>> GetTripDetails(int tripId, bool includeExpenses = true) =>
        await _tripsService.GetTripDetailsAsync(tripId, UserId, includeExpenses);
}


