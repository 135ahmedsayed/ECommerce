namespace Ecommerce.Shared.DTOs.Products;
public class ProductQueryParameter
{
    public int? BrandId { get; set; }
    public int? TypeId { get; set; }
    public string? Search { get; set; }
    public ProductSortOption Sort { get; set; }
    // Pagination properties
    private const int maxPageSize = 10; 
    private const int DefaultPageSize = 5;
    private int pageSize = DefaultPageSize;
    public int PageSize 
    { 
        get => pageSize;
        set => pageSize = value > maxPageSize ? maxPageSize : 
            value <  DefaultPageSize ? DefaultPageSize : value;
    }
    public int PageIndex { get; set; } = 1; 
}

// Enum for sorting options
public enum ProductSortOption
{
    NameAsc = 1,
    NameDesc = 2,
    PriceAsc = 3,
    PriceDesc = 4
}