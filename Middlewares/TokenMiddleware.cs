using api.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace api.Middlewares;

public static class TokenMiddleware
{
    public static AuthenticationBuilder UseJwtAuthentication(this IHostApplicationBuilder builder)
    {
        return builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = TokenServiceExtensions.GetTokenValidationParameters(
                builder.Configuration["JWT:Issuer"],
                builder.Configuration["JWT:AccessSecret"]!
            );

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception is SecurityTokenExpiredException)
                    {
                        return context.WriteAuthenticationFailedResponse("Expired Token");
                    }

                    if (context.Exception is SecurityTokenInvalidSignatureException)
                    {
                        return context.WriteAuthenticationFailedResponse("Invalid Token");
                    }

                    return context.WriteAuthenticationFailedResponse("Token Authentication Failed");
                },

                OnChallenge = context =>
                {
                    if (!context.Response.HasStarted)
                    {
                        return context.WriteChallengeResponse("Invalid Token");
                    }
                    return Task.CompletedTask;
                }
            };
        }
        );
    }

    private static Task WriteAuthenticationFailedResponse(this ResultContext<JwtBearerOptions> context, string message)
    {
        context.Response.StatusCode = 401;
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(JsonSerializer.Serialize(new
        {
            error = message
        }));
    }

    private static Task WriteChallengeResponse(this PropertiesContext<JwtBearerOptions> context, string message)
    {
        context.Response.StatusCode = 401;
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(JsonSerializer.Serialize(new
        {
            error = message
        }));
    }
}
