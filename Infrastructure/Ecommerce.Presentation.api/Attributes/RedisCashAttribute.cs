using Microsoft.AspNetCore.Mvc.Filters;

namespace Ecommerce.Presentation.api.Attributes;

public class RedisCashAttribute
    : ActionFilterAttribute
{
    public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        next.Invoke();
        return base.OnActionExecutionAsync(context, next);
    }
}
