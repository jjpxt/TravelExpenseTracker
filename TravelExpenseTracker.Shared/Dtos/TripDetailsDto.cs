namespace TravelExpenseTracker.Shared.Dtos;

public record TripDetailsDto(TripListDto TripInfo, ExpenseListDto[] Expenses);
