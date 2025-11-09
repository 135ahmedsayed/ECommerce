using AutoMapper;
using Ecommerce.Domain.Entities.Auth;
using Ecommerce.ServiceAbstraction;
using Ecommerce.ServiceAbstraction.Common;
using Ecommerce.Shared.DTOs.Auth;
using Ecommerce.Shared.DTOs.Users;
using ECommerce.Infrastructure.Service.Contracts;
using ECommerce.ServicesAbstractions.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.Service;
public class UserService(UserManager<ApplicationUser> userManager ,
    ITokenService tokenService ,
    IMapper mapper) 
    : IUserService
{
    public async Task<Result<UserResponse>> GetByEmailAsync(string email)
    {
        var user =await userManager.FindByEmailAsync(email);
        if (user == null)
            return Error.NotFound("User not found" ,$"User with Email {email} was not found");
        var roles = await userManager.GetRolesAsync(user);
        return new UserResponse(user.Email, user.DisplayName,tokenService.GetToken(user,roles));
    }
    public async Task<Result<AddressDTO>> GetAddressAsync(string email)
    {
        var user = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(x => x.Email == email);
        if (user == null)
            return Error.NotFound("User not found", $"User with Email {email} was not found");
        if (user.Address == null)
            return Error.NotFound("Address not found", $"User with Email {email} does't have Address");
        return mapper.Map<AddressDTO>(user.Address);

    }


    public async Task<Result<AddressDTO>> UpdateAddressAsync(string email, AddressDTO address)
    {
        var user = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(x => x.Email == email);
        if (user == null)
            return Error.NotFound("User not found", $"User with Email {email} was not found");
        if (user.Address != null)
        {
            user.Address.FirstName = address.FirstName;
            user.Address.LastName = address.LastName;
            user.Address.City = address.City;
            user.Address.Street = address.Street;
        }
        else
        {
            user.Address = mapper.Map<Address>(address);
        }
        await userManager.UpdateAsync(user);
        return mapper.Map<AddressDTO>(user.Address);
    }
}
