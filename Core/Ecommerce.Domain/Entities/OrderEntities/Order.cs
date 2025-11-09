using Ecommerce.Domain.Entities.Products;

namespace Ecommerce.Domain.Entities.OrderEntities;
public class Order : Entity<Guid>
{
    public ICollection<OrderItem> Items { get; set; } = [];
    public DeliveryMethod? deliveryMethod { get; set; } 
    public int? DeliveryMethodId { get; set; }
    public decimal Subtotal { get; set; }
    public string UserEmail { get; set; } = default!;
    public OrderAddress Address { get; set; } = default!;
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public PaymentStatus Status { get; set; } = PaymentStatus.pending;
    public string PaymentIntentId { get; set; } = string.Empty;
}
public class OrderAddress
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string Country { get; set; } = default!;
}
