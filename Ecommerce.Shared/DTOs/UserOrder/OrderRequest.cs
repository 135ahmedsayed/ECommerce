using Ecommerce.Shared.DTOs.Users;

namespace Ecommerce.Shared.DTOs.UserOrder;
public record OrderRequest(AddressDTO Address ,string basketId ,int DeliveryMethodId);
