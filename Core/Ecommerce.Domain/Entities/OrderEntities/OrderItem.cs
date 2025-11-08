using Ecommerce.Domain.Entities.Products;

namespace Ecommerce.Domain.Entities.OrderEntities;
public class OrderItem : Entity<Guid>
{
#nullable disable
    public ProductInOrderItem Product { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Guid OrderId { get; set; }
}

public class ProductInOrderItem
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string PictureUrl { get; set; }

}
