namespace api.Dtos.Auth;

public class AuthResponse : BaseResponse<AuthDto>
{
    public new int StatusCode { get; set; } = 200;
}
