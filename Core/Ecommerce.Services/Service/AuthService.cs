using System.Data;
using Ecommerce.Domain.Entities.Auth;
using Ecommerce.ServiceAbstraction;
using Ecommerce.ServiceAbstraction.Common;
using Ecommerce.Shared.DTOs.Auth;
using ECommerce.Infrastructure.Service.Contracts;
using ECommerce.ServicesAbstractions.Common;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Services.Service;

public class AuthService(UserManager<ApplicationUser> userManager , ITokenService tokenService)
    : IAuthService
{
    public async Task<bool> CheckEmailAsync(string email)
    => await userManager.FindByEmailAsync(email) != null;

    public async Task<Result<UserResponse>> LoginAsync(LoginRequest request)
    {
        var user =await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return Error.Unauthorized(description: "Invalid Email or Password");
        var result = await userManager.CheckPasswordAsync(user, request.Password);
        if (!result)
            return Error.Unauthorized(description: "Invalid Email or Password");

        var roles = await userManager.GetRolesAsync(user);
        var token = tokenService.GetToken(user, roles);
        return new UserResponse(user.Email, user.DisplayName, token);
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
        if(!result.Succeeded)
            return result.Errors.Select(e => Error.Validation(e.Code ,e.Description))
                .ToList();
        var token = tokenService.GetToken(user, []);

        return new UserResponse(user.Email, user.DisplayName, token);
    }
}
