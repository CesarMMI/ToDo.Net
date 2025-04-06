using api.Models;
using System.Security.Claims;

namespace api.Interfaces;

public interface ITokenService
{
    string CreateAccessToken(User user);
    string CreateRefreshToken(User user);
    ClaimsPrincipal ValidateRefreshToken(string token);
}
