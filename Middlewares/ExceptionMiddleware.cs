using api.Dtos;
using api.Exceptions;

namespace api.Middlewares;

public class ExceptionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (AppException e)
        {
            await ReturnErrorAsync(context, e.StatusCode, e.Message);
        }
        catch (Exception)
        {
            await ReturnErrorAsync(context, 500, "Internal server error");
        }
    }

    private static async Task ReturnErrorAsync(HttpContext context, int statusCode, string message)
    {
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(new BaseResponse { StatusCode = statusCode, Message = message });
    }
}

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}
