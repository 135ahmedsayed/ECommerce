namespace Ecommerce.Domain.Entities.Basket;

public class BasketItem
{
#nullable disable
    public int Id { get; set; }
    public string Name { get; set; } 
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string PictureUrl { get; set; }
}
