using Ecommerce.Domain.Entities.Products;

namespace Ecommerce.Services.Specifications;

internal class ProductWithBrandTypeSpecification : BaseSpecification<Product>
{
    
    public ProductWithBrandTypeSpecification()
        : base(null!)
    {
        AddInclude(p => p.ProductType);
        AddInclude(p => p.ProductBrand);
    }

    // Criteria to filter by id
    public ProductWithBrandTypeSpecification(int id)
        : base(p => p.Id == id)
    {
        AddInclude(p => p.ProductType);
        AddInclude(p => p.ProductBrand);
    }
}
