using Ecommerce.Services.Service.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Handler;

public class ExceptionHendler(ILogger<ExceptionHendler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is NotFoundException notFound)
        {
            logger.LogError($"Message was Wrong {notFound.Message}"); //logging
            //write response
            //set http response status code
            // create a response object
            var problem = new ProblemDetails
            {
                Title = "An error occurred while processing your request.",
                Detail = notFound.Message,
                Instance = httpContext.Request.Host.ToString(),
                Status = StatusCodes.Status404NotFound
            };
            httpContext.Response.StatusCode = problem.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(problem,cancellationToken);
            return true;
        }
        return false;
    }
}
