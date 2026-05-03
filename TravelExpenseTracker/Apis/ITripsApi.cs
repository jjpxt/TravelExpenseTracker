using Refit;
using TravelExpenseTracker.Shared.Dtos;

namespace TravelExpenseTracker.Apis;

[Headers("Authorization: Bearer ")]
public interface ITripsApi
{
    [Get("/api/trips/categories")]
    Task<CategoryDto[]> GetCategories();

    [Post("/api/trips")]
    Task<ApiResult> SaveTrip(SaveTripDto dto);

    [Get("/api/trips")]
    Task<TripListDto[]> GetUserTrips(int count = 100);

    [Get("/api/trips/{tripId}")]
    Task<ApiResult<TripDetailsDto>> GetTripDetails(int tripId, bool includeExpenses = true);
}