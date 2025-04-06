using api.Exceptions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api.Services;

public class TokenService : ITokenService
{
    private readonly JwtSecurityTokenHandler tokenHandler;

    private readonly string? issuer;
    private readonly string? accessSecret;
    private readonly string? refreshSecret;

    public TokenService(IConfiguration configuration)
    {
        tokenHandler = new JwtSecurityTokenHandler();
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        issuer = configuration.GetValue<string>("JWT:Issuer");
        accessSecret = configuration.GetValue<string>("JWT:AccessSecret");
        refreshSecret = configuration.GetValue<string>("JWT:RefreshSecret");


    }

    public string CreateAccessToken(User user)
    {
        return CreateToken(user, accessSecret!, DateTime.UtcNow.AddMinutes(1));
    }

    public string CreateRefreshToken(User user)
    {
        return CreateToken(user, refreshSecret!, DateTime.UtcNow.AddMinutes(5));
    }

    public ClaimsPrincipal ValidateRefreshToken(string token)
    {
        var validationParameters = TokenServiceExtensions.GetTokenValidationParameters(issuer, refreshSecret!);

        try
        {
            return tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
        }
        catch (SecurityTokenException ex)
        {
            throw new UnauthorizedException("Invalid token");
        }
    }

    private string CreateToken(User user, string secret, DateTime expires)
    {
        var claims = new List<Claim> {
            new("name", user.Username),
            new("sub", user.Email)
        };
        var key = TokenServiceExtensions.GetSymmetricSecurityKey(secret);
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: issuer,
            claims: claims,
            expires: expires,
            signingCredentials: credentials
        );

        return tokenHandler.WriteToken(tokenDescriptor);
    }
}

public static class TokenServiceExtensions
{
    public static AuthenticationBuilder UseJwtAuthentication(this WebApplicationBuilder builder)
    {
        return builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            options.TokenValidationParameters = GetTokenValidationParameters(builder.Configuration["JWT:Issuer"], builder.Configuration["JWT:AccessSecret"]!)
        );
    }

    public static TokenValidationParameters GetTokenValidationParameters(string? issuer, string secret)
    {
        return new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = GetSymmetricSecurityKey(secret)
        };
    }

    public static SymmetricSecurityKey GetSymmetricSecurityKey(string secret)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
    }
}

