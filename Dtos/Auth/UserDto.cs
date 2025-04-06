namespace api.Dtos.Auth;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;

    public static UserDto FromModel(Models.User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
        };
    }
}
