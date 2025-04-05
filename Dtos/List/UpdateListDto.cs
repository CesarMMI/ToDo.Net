using System.ComponentModel.DataAnnotations;

namespace api.Dtos.List;

public class UpdateListDto
{
    [Required]
    [MinLength(1)]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(100)]
    public string Description { get; set; } = string.Empty;
}
