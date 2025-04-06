using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Auth;

public class RegisterRequest
{
    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string Password { get; set; } = string.Empty;
}
