using System.ComponentModel.DataAnnotations;
using TravelExpenseTracker.Shared;

namespace TravelExpenseTracker_API.Data.Entities;

public class Trip
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string Title { get; set; }

    [Required, MaxLength(250)]
    public string Location { get; set; }

    public int UserId { get; set; }
    public virtual User User{ get; set; }

    public int CategoryId { get; set; }
    public virtual TripCategory Category { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    [Required, MaxLength(15)]
    public string Status { get; set; } = nameof(TripStatus.Planned);
    public virtual ICollection<TripExpense> Expenses { get; set; } = [];
}
