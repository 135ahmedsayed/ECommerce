using Ecommerce.ServiceAbstraction.Common;
using Ecommerce.Shared.DTOs.Baskets;
using Ecommerce.Shared.DTOs.UserOrder;

namespace Ecommerce.ServiceAbstraction;
public interface IOrderService
{
    Task<Result<OrderResponse>> CreateAsync(OrderRequest request, string email);

    Task<Result<OrderResponse>> GetByIdAsync(Guid Id);
    Task<IEnumerable<OrderResponse>> GetByUserEmailAsync(string email);
    Task<IEnumerable<DeliveryMethodResponse>> GetDeliveryMethodsync();

}
