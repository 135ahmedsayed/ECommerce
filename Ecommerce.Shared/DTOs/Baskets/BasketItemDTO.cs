namespace Ecommerce.Shared.DTOs.Baskets;
public class BasketItemDTO
{
#nullable disable
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string PictureUrl { get; set; }
}
