using Ecommerce.ServiceAbstraction.Common;
using Ecommerce.Shared.DTOs.Auth;
using Ecommerce.Shared.DTOs.Users;

namespace Ecommerce.ServiceAbstraction;
public interface IUserService
{
    Task<Result<UserResponse>> GetByEmailAsync(string email);
    Task<Result<AddressDTO>> GetAddressAsync(string email);
    Task<Result<AddressDTO>> UpdateAddressAsync(string email , AddressDTO address);
    
}
