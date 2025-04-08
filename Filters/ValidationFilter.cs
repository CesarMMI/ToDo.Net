using api.ErrorHandler.Middlewares;
using api.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace api.Filters;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var firstError = context.ModelState
                .Where(e => e.Value?.Errors.Count > 0)
                .Select(e => new
                {
                    Field = e.Key,
                    Message = e.Value?.Errors.First().ErrorMessage
                })
                .FirstOrDefault();

            if (firstError is not null)
            {
                if (firstError.Message is null)
                {
                    throw new InternalServerErrorException();
                }
                throw new ValidationException(firstError.Message);
            }
        }
    }

    public void OnActionExecuting(ActionExecutingContext context) { }
}

public static class ValidationFilterExtensions
{
    public static IHostApplicationBuilder AddControllersWithDataValidation(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<ValidationFilter>();
        });

        return builder;
    }
}
