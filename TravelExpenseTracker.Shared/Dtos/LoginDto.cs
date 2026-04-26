using System.ComponentModel.DataAnnotations;

namespace TravelExpenseTracker.Shared.Dtos;

public class LoginDto
{
    [Required, MaxLength(100)]
    public string Email { get; set; }

    [Required, MaxLength(30)]
    public string Password { get; set; }
}