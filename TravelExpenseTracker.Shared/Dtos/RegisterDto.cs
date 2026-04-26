using System.ComponentModel.DataAnnotations;

namespace TravelExpenseTracker.Shared.Dtos;

public class RegisterDto
{
    [Required, MaxLength(50)]
    public string Name { get; set; }

    [Required, MaxLength(100)]
    public string Email { get; set; }

    [Required, MaxLength(30)]
    public string Password { get; set; }
}
