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
            //not found Endpoint
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var problem = new ProblemDetails
                {
                    Title = "Endpoint Not Found",
                    Status = StatusCodes.Status404NotFound,
                    Detail = $"The requested endpoint {context.Request.Path} was not found on this server.",
                    Instance = context.Request.Path
                };
                await context.Response.WriteAsJsonAsync(problem);
            }
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
