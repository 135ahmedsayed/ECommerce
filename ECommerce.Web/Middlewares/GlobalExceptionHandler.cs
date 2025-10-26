using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Middlewares;
//MiddleWare
public class GlobalExceptionHandler(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context ,
        ILogger<GlobalExceptionHandler> logger)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError($"Message was Wrong {ex.Message}"); //logging
            //write response
            //set http response status code
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            // create a response object
            var problem = new ProblemDetails
            {
                Title = "An error occurred while processing your request.",
                Status = StatusCodes.Status500InternalServerError,
                Detail = ex.Message,
                Instance = context.Request.Host.ToString()
            };
            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}

//middleware two
public static class GlobalExceptionHandlerExtensions
{
    public static WebApplication UseCustomExceptionHandler(this WebApplication app)
    {
        app.UseMiddleware<GlobalExceptionHandler>();
        return app;
    }
}
