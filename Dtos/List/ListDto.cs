namespace api.Dtos.List;

public class ListDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public static ListDto FromModel(Models.List list)
    {
        return new ListDto
        {
            Id = list.Id,
            Name = list.Name,
            Description = list.Description,
            CreatedAt = list.CreatedAt,
            UpdatedAt = list.UpdatedAt,
        };
    }
}
