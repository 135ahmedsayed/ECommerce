
using Ecommerce.Domain.Entities.Auth;

namespace ECommerce.Infrastructure.Service.Contracts;

public interface ITokenService
{
    string GetToken(ApplicationUser user, IList<string> roles);
}
