using System.ComponentModel.DataAnnotations;

namespace TravelExpenseTracker_API.Data.Entities;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; }

    [Required, MaxLength(80)]
    public string Email { get; set; }

    [Required, MaxLength(500)]
    public string PasswordHash { get; set; }
}
