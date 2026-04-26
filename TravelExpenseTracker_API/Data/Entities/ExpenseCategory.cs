using System.ComponentModel.DataAnnotations;

namespace TravelExpenseTracker_API.Data.Entities;

public class ExpenseCategory
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; }
    public int? UserId { get; set; }
    public virtual User? User { get; set; }

    public static ExpenseCategory Create(int id, string name) =>
        new()
        {
            Id = id,
            Name = name
        };

    public static ExpenseCategory[] GetSeedData() =>
        [
            ExpenseCategory.Create(1, "Tickets"),
            ExpenseCategory.Create(2, "Shopping"),
            ExpenseCategory.Create(3, "Food"),
            ExpenseCategory.Create(4, "Fuel"),
            ExpenseCategory.Create(5, "Other"),
            ];

}
