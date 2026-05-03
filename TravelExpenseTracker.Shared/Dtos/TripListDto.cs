namespace TravelExpenseTracker.Shared.Dtos;

public record TripListDto(int Id, string Title, string Image, string Status, string Location, DateTime StartDate, DateTime EndDate, int CategoryId)
{
    public string DisplayDateRange => $"{StartDate:dd/MM/yy} to {EndDate:dd/MM/yy}";

    public static TripListDto Empty() =>
        new(default, default, default, default, default, default, default, default);
}
