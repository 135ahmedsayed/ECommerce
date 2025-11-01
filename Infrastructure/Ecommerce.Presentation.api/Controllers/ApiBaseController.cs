using Ecommerce.ServiceAbstraction.Common;
using ECommerce.ServicesAbstractions.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ecommerce.Presentation.api.Controllers;
[ApiController]
[Route("api/[Controller]")]
public class ApiBaseController : ControllerBase
{
    protected IActionResult HandleResult(Result result)
    {
        if (result.IsSuccess)
            return NoContent();
        return Problem(result.Errors);
    }
    protected ActionResult<TValue> HandleResult<TValue>(Result<TValue> result)
    {
        if (result.IsSuccess)
            return NoContent();
        return Problem(result.Errors);
    }

    private ActionResult Problem(IReadOnlyList<Error> errors)
    {
        if (errors.Count == 0)
            return Problem(statusCode: 500, title: "An unexpected error occurred");
        if(errors.All(e => e.Type == ErrorType.Validation))
            return handleValidationErrors(errors);
        return HandleSingleErrorProblem(errors[0]);
    }
    private ActionResult handleValidationErrors(IReadOnlyList<Error> errors)
    {
        var modelState = new ModelStateDictionary();
        foreach (var error in errors)
        {
            modelState.AddModelError(error.Code, error.Description);
        }
        return ValidationProblem(modelState);
    }
    private ActionResult HandleSingleErrorProblem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };
        return Problem(statusCode: statusCode, title: error.Description , type : error.Code);
    }
}
