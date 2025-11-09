using Ecommerce.ServiceAbstraction.Common;
using Ecommerce.Shared.DTOs.UserOrder;

namespace Ecommerce.ServiceAbstraction;
public interface IOrderService
{
    Task<Result<OrderResponse>> CreateAsync(OrderRequest request, string email);
}
