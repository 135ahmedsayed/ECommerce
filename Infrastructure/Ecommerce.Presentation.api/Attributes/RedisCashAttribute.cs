using System.Text;
using Ecommerce.ServiceAbstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Presentation.api.Attributes;

internal class RedisCashAttribute(int durationInMin = 2)
    : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        //1- Get CashService using DI
        var cashService = context.HttpContext.RequestServices.GetRequiredService<ICashService>();
        //2- Create Cash Key
        string key = GenerateCacheKey(context.HttpContext.Request);
        //3- searchValue in Redis Cash
        var cashValue =await cashService.GetAsync(key);
        //4- if found return it
        if (cashValue is not null)
        {
            context.Result = new ContentResult
            {
                Content = cashValue,
                ContentType = "application/json",
                StatusCode = StatusCodes.Status200OK
            };
            return;
        }
        //5- if not found execute the action
        var actionExecutedContext = await next.Invoke();
        var result = actionExecutedContext.Result;
        //6- Cash the result
        if (result is OkObjectResult okResult)
        {
            await cashService.SetAsync(key, okResult.Value!, TimeSpan.FromMinutes(durationInMin));
        }
    }

    private static string GenerateCacheKey(HttpRequest request)
    {
        var sb = new StringBuilder();
        foreach (var kyp in request.Query.OrderBy(q => q.Key))
        {
            sb.Append($"{kyp.Key}-{kyp.Value}-");
        }
        return sb.ToString().Trim('-');
    }
}
