namespace api.Dtos.Task;

public class UpdateTaskDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? ListId { get; set; }
    public bool Done { get; set; }
}
