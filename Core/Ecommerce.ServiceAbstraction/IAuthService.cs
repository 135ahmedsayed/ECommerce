using Ecommerce.ServiceAbstraction.Common;
using Ecommerce.Shared.DTOs.Auth;

namespace Ecommerce.ServiceAbstraction;

public interface IAuthService
{
    Task<Result<UserResponse>> LoginAsync(LoginRequest request);
    Task<Result<UserResponse>> RegisterAsync(RegisterRequest request);
}
