namespace Ecommerce.Domain.Entities.Products;
public class Product : Entity<int>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string PictureUrl { get; set; } = default!;
    public decimal Salary { get; set; } = default!;

    //relationships
    public ProductBrand ProductBrand { get; set; } = default!;
    public int BrandId { get; set; } = default!;
    public ProductType ProductType { get; set; } = default!;
    public int TypeId { get; set; } = default!;

}