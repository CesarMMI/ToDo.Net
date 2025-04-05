using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Task;

public class UpdateTaskDto
{
    [Required]
    [MinLength(1)]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(100)]
    public string Description { get; set; } = string.Empty;
    public int? ListId { get; set; }
    public bool Done { get; set; }
}
