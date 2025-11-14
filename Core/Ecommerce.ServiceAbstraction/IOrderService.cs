using Ecommerce.ServiceAbstraction.Common;
using Ecommerce.Shared.DTOs.Baskets;
using Ecommerce.Shared.DTOs.UserOrder;

namespace Ecommerce.ServiceAbstraction;
public interface IOrderService
{
    Task<Result<OrderResponse>> CreateAsync(OrderRequest request, string email, CancellationToken cancellationToken);

    Task<Result<OrderResponse>> GetOrderAsync(Guid Id , string email, CancellationToken cancellationToken);
    Task<IEnumerable<OrderResponse>> GetAllAsync(string email, CancellationToken cancellationToken);
    Task<IEnumerable<DeliveryMethodResponse>> GetDeliveryMethodsync(CancellationToken cancellationToken);

}
