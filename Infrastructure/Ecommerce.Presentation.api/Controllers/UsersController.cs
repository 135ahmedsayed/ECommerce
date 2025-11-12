using System.Security.Claims;
using Ecommerce.ServiceAbstraction;
using Ecommerce.Shared.DTOs.Auth;
using Ecommerce.Shared.DTOs.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Presentation.api.Controllers;

[ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
[Authorize]
public class UsersController(IUserService userService)
    : ApiBaseController
{
    [HttpGet]
    public async Task<ActionResult<UserResponse>> GetCurrentUser()
    {
        string email = User.FindFirstValue(ClaimTypes.Email)!;
        var result = await userService.GetByEmailAsync(email);
        return HandleResult(result);
    }
    [HttpGet("Address")]
    public async Task<ActionResult<AddressDTO>> GetUserAddress()
    {
        string email = User.FindFirstValue(ClaimTypes.Email)!;
        var result = await userService.GetAddressAsync(email);
        return HandleResult(result);
    }

    [ProducesResponseType<AddressDTO>(200)]
    [HttpPut("Address")]
    public async Task<ActionResult<AddressDTO>> UpdateUserAddress(AddressDTO address)
    {
        string email = User.FindFirstValue(ClaimTypes.Email)!;
        var result = await userService.UpdateAddressAsync(email, address);
        return HandleResult(result);
    }
}
