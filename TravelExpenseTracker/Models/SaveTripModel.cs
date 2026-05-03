using CommunityToolkit.Mvvm.ComponentModel;
using TravelExpenseTracker.Shared;
using TravelExpenseTracker.Shared.Dtos;

namespace TravelExpenseTracker.Models;

public partial class SaveTripModel : ObservableObject
{
    [ObservableProperty]
    private int _id;
    [ObservableProperty]
    public int _categoryId;
    [ObservableProperty]
    private string _title;
    [ObservableProperty]
    private string _location;
    [ObservableProperty]
    private string _status = nameof(TripStatus.Planned);
    [ObservableProperty]
    private DateTime _startDate = DateTime.Now;
    [ObservableProperty]
    private DateTime _endDate = DateTime.Now.AddDays(1);

    public (bool IsValid, string? Error) Validate()
    {
        string? error = null;
        if (CategoryId == 0)
            error = "Category selection is required";

        else if (string.IsNullOrWhiteSpace(Title)
            || string.IsNullOrWhiteSpace(Location)
            || string.IsNullOrWhiteSpace(Status)
            || StartDate == default
            || EndDate == default)
            error = "All fields required";

        else if (EndDate < StartDate)
            error = "End date cannot be less than start date";

        return (error == null , error);
    }

    public SaveTripDto ToDto() =>
        new SaveTripDto
        {
            CategoryId = CategoryId,
            EndDate = EndDate,
            Id = Id,
            Location = Location,
            StartDate = StartDate,
            Status = Status,
            Title = Title
        };

    public static SaveTripModel FromDto(SaveTripDto dto) =>
        new SaveTripModel
        {
            CategoryId = dto.CategoryId,
            EndDate = dto.EndDate,
            Id = dto.Id,
            Location = dto.Location,
            StartDate = dto.StartDate,
            Status = dto.Status,
            Title = dto.Title
        };
}
