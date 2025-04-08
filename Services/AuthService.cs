using api.Dtos.Auth;
using api.Exceptions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;

namespace api.Services;

public class AuthService(ITokenService tokenService, IUserRepository userRepository) : IAuthService
{
    private readonly PasswordHasher<User> passwordHasher = new();

    public async Task<AuthDto> Login(LoginRequest request)
    {
        var user = await userRepository.FindByEmailAsync(request.Email);

        if (user is null)
        {
            throw new BadRequestException("Email or password is wrong");
        }

        var validPassword = passwordHasher.VerifyHashedPassword(user, user.Password, request.Password) == PasswordVerificationResult.Success;

        if (!validPassword)
        {
            throw new BadRequestException("Email or password is wrong");
        }

        string accessToken = tokenService.CreateAccessToken(user);
        string refreshToken = tokenService.CreateRefreshToken(user);

        return AuthDto.FromModel(user, accessToken, refreshToken);
    }

    public async Task<AuthDto> Refresh(RefreshRequest request)
    {
        var claims = tokenService.ValidateRefreshToken(request.RefreshToken);
        var sub = claims.Claims.FirstOrDefault(c => c.Type == "sub");

        var user = await userRepository.FindByIdAsync(request.UserId);

        if (user is null || sub?.Value != user.Email)
        {
            throw new UnauthorizedException("Invalid token");
        }

        string accessToken = tokenService.CreateAccessToken(user);

        return AuthDto.FromModel(user, accessToken, request.RefreshToken);
    }

    public async Task<AuthDto> Register(RegisterRequest request)
    {
        if (await userRepository.ExistsByEmailAsync(request.Email))
        {
            throw new BadRequestException("Email already in use");
        }

        var user = new User
        {
            Username = request.Username,
            Email = request.Email
        };
        user.Password = passwordHasher.HashPassword(user, request.Password);

        user = await userRepository.CreateAsync(user);

        string accessToken = tokenService.CreateAccessToken(user);
        string refreshToken = tokenService.CreateRefreshToken(user);

        return AuthDto.FromModel(user, accessToken, refreshToken);
    }
}
