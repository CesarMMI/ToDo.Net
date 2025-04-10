using api.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace api.Middlewares;

public class DataValidationMiddleware : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
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
                if (firstError.Message is not null)
                {
                    throw new BadRequestException(firstError.Message);
                }
                throw new InternalServerErrorException();
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}

public static class DataValidationMiddlewareExtensions
{
    public static IHostApplicationBuilder AddControllersWithDataValidation(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<DataValidationMiddleware>();
        });

        return builder;
    }
}
