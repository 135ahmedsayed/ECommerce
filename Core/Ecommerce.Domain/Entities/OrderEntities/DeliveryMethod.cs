using Ecommerce.Domain.Entities.Products;

namespace Ecommerce.Domain.Entities.OrderEntities;
public class DeliveryMethod : Entity<int>
{
    public string ShortName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public string DeliveryTime { get; set; } = default!;
    
}
