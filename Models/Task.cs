namespace api.Models;

public class Task
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Done { get; set; } = false;
    public DateTime? DoneAt { get; set; }

    public int? ListId { get; set; }
    public List? List { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}