using api.Models;

namespace api.Dtos.Auth;

public class AuthDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public UserDto User { get; set; }

    public static AuthDto FromModel(User user, string accessToken, string refreshToken)
    {
        return new AuthDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            User = UserDto.FromModel(user)
        };
    }
}
