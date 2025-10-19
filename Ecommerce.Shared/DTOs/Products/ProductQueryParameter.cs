namespace Ecommerce.Shared.DTOs.Products;
public class ProductQueryParameter
{
    public int? BrandId { get; set; }
    public int? TypeId { get; set; }
    public string? Search { get; set; }
    public ProductSortOption Sort { get; set; }
}

// Enum for sorting options
public enum ProductSortOption
{
    NameAsc = 1,
    NameDesc = 2,
    PriceAsc = 3,
    PriceDesc = 4
}