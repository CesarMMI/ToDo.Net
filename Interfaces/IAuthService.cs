using api.Dtos.Auth;

namespace api.Interfaces;

public interface IAuthService
{
    Task<AuthDto> Login(LoginRequest request);
    Task<AuthDto> Refresh(RefreshRequest request);
    Task<AuthDto> Register(RegisterRequest request);
}
