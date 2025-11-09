using Ecommerce.Shared.DTOs.Users;

namespace Ecommerce.Shared.DTOs.UserOrder;
public class OrderResponse
{
    public Guid Id { get; set; }
    public string UserEmail { get; set; } = default!;
    public string deliveryMethod { get; set; } = default!;
    public decimal DeliveryMethodCost { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Total { get; set; }
    public AddressDTO Address { get; set; } = default!;
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public string Status { get; set; } = default!;
    public string PaymentIntentId { get; set; } = string.Empty;
    public ICollection<OrderItemDTO> Items { get; set; } = [];
}
