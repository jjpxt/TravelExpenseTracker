using System.ComponentModel.DataAnnotations;

namespace TravelExpenseTracker.Shared.Dtos;

public class ExpenseDto
{
    public int Id { get; set; }

    [Required, MaxLength(250)]
    public string Title { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Category Selection Is Required")]
    public int CategoryId { get; set; }

    [Range(0.1, double.MaxValue, ErrorMessage = "Invalid Amount")]
    public decimal Amount { get; set; }
    public DateTime SpentOn { get; set; }
}
