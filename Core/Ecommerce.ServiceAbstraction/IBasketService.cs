using Ecommerce.Shared.DTOs.Baskets;

namespace Ecommerce.ServiceAbstraction;
public interface IBasketService
{
    Task<CustomerBasketDTO> CreateOrUpdateAsync(CustomerBasketDTO basketDTO);
    Task<CustomerBasketDTO> GetByIdAsync(string id);
    Task DeleteAsync(string id);
}
