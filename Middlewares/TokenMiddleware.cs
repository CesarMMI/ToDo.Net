using api.Dtos;
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
                        return context.Response.WriteResponse("Token expired");
                    }

                    if (context.Exception is SecurityTokenInvalidSignatureException)
                    {
                        return context.Response.WriteResponse("Invalid token");
                    }

                    return context.Response.WriteResponse("Token authentication failed");
                },

                OnChallenge = context =>
                {
                    if (!context.Response.HasStarted)
                    {
                        return context.Response.WriteResponse("Invalid token");
                    }
                    return Task.CompletedTask;
                }
            };
        }
        );
    }

    private static Task WriteResponse(this HttpResponse response, string message)
    {
        response.StatusCode = 401;
        response.ContentType = "application/json";
        return response.WriteAsync(JsonSerializer.Serialize(
            new BaseResponse { StatusCode = 401, Message = message },
            new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
        ));
    }
}
