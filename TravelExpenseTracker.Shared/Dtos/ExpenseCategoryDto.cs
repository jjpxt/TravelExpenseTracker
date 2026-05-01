using System.ComponentModel.DataAnnotations;

namespace TravelExpenseTracker.Shared.Dtos;

public class ExpenseCategoryDto
{
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; }
    public int? UserId { get; set; }
}
