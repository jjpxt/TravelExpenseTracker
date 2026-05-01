using System.ComponentModel.DataAnnotations;

namespace TravelExpenseTracker.Shared.Dtos;

public class SaveTripDto
{
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Category Selection Is Required")]
    public int CategoryId { get; set; }

    [Required, MaxLength(100)]
    public string Title { get; set; }

    [Required, MaxLength(250)]
    public string Location { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    [Required, MaxLength(100)]
    public string Status { get; set; }
}
