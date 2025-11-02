using Ecommerce.Domain.Entities.Auth;
using Ecommerce.ServiceAbstraction;
using Ecommerce.ServiceAbstraction.Common;
using Ecommerce.Shared.DTOs.Auth;
using ECommerce.ServicesAbstractions.Common;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Services.Service;

public class AuthService(UserManager<ApplicationUser> userManager)
    : IAuthService
{
    public async Task<Result<UserResponse>> LoginAsync(LoginRequest request)
    {
        var user =await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return Error.Unauthorized(description: "Invalid Email or Password");
        var result = await userManager.CheckPasswordAsync(user, request.Password);
        if (!result)
            return Error.Unauthorized(description: "Invalid Email or Password");

        return new UserResponse(user.Email, user.DisplayName, "Token");
    }

    public async Task<Result<UserResponse>> RegisterAsync(RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            Email = request.Email,
            UserName = request.UserName!,
            DisplayName = request.DisplayName,
            PhoneNumber = request.PhoneNumber
        };
        var result = await userManager.CreateAsync(user, request.Password);
        if(result.Succeeded)
            return new UserResponse(user.Email, user.DisplayName, "Token");
        return result.Errors.Select(e => Error.Validation(e.Code ,e.Description))
            .ToList();
    }
}
