namespace Ecommerce.Shared.DTOs.UserOrder;
public class OrderItemDTO
{
#nullable disable
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string PictureUrl { get; set; }
}
