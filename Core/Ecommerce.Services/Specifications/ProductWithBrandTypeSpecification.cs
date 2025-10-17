using Ecommerce.Domain.Entities.Products;

namespace Ecommerce.Services.Specifications;

internal class ProductWithBrandTypeSpecification : BaseSpecification<Product>
{
    public ProductWithBrandTypeSpecification()
    {
        AddInclude(p => p.ProductBrand);
        AddInclude(p => p.ProductType);
    }
}
