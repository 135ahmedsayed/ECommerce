namespace Ecommerce.Shared.DTOs.Products;
public record ProductResponse()
{
#nullable disable
    public int Id { get; set; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string PictureUrl { get; init; }
    public decimal Price { get; init; }
    public string Brand { get; init; }
    public string Type { get; init; }
}
