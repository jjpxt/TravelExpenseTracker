using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelExpenseTracker_API.Data.Entities;

public class TripExpense
{
    [Key]
    public long Id { get; set; }
    public int TripId { get; set; }
    public virtual Trip Trip { get; set; }
    public int ExpenseCategoryId { get; set; }

    public virtual ExpenseCategory ExpenseCategory{ get; set; }

    [Required, MaxLength(150)]
    public string Title { get; set; }

    [Column(TypeName = "DECIMAL(18,2)")]
    public decimal Amount { get; set; }
    public DateTime SpentOn { get; set; }
}