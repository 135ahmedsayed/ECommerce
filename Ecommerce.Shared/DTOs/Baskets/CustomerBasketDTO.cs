namespace Ecommerce.Shared.DTOs.Baskets;

public class CustomerBasketDTO
{
    public string Id { get; set; }
    public ICollection<BasketItemDTO> Items { get; set; }
}
