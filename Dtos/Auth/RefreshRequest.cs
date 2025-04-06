using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Auth;

public class RefreshRequest
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}
