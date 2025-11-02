using Ecommerce.ServiceAbstraction;
using Ecommerce.Shared.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Presentation.api.Controllers;
public class AuthController(IAuthService authService)
    :ApiBaseController
{
    //register
    [HttpPost("Register")]
    public async Task<ActionResult<UserResponse>> Register(RegisterRequest request)
    {
        var result = await authService.RegisterAsync(request);
        return HandleResult(result);
    }
    //login
    [HttpPost("login")]
    public async Task<ActionResult<UserResponse>> Login(LoginRequest request)
    {
        var result = await authService.LoginAsync(request);
        return HandleResult(result);
    }
}
