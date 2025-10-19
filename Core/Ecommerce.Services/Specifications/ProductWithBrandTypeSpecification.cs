using System.Linq.Expressions;
using Ecommerce.Domain.Entities.Products;
using Ecommerce.Shared.DTOs.Products;

namespace Ecommerce.Services.Specifications;

internal class ProductWithBrandTypeSpecification : BaseSpecification<Product>
{

    public ProductWithBrandTypeSpecification(ProductQueryParameter parameters)
        : base(CreateCriteira(parameters))
    {
        AddInclude(p => p.ProductType);
        AddInclude(p => p.ProductBrand);

        // Sorting
        Sorting(parameters);
    }
    

    private static Expression<Func<Product, bool>> CreateCriteira(ProductQueryParameter parameters)
    {
        return p => (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId.Value)
                    && (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId.Value)
                    && (string.IsNullOrWhiteSpace(parameters.Search) || p.Name.Contains(parameters.Search!));
    }

    // Criteria to filter by id
    public ProductWithBrandTypeSpecification(int id)
        : base(p => p.Id == id)
    {
        AddInclude(p => p.ProductType);
        AddInclude(p => p.ProductBrand);
    }


    // Sorting
    private void Sorting(ProductQueryParameter parameters)
    {
        switch (parameters.Sort)
        {
            case ProductSortOption.NameAsc:
                AddOrderBy(p => p.Name);
                break;
            case ProductSortOption.NameDesc:
                AddOrderByDesc(p => p.Name);
                break;
            case ProductSortOption.PriceAsc:
                AddOrderBy(p => p.Price);
                break;
            case ProductSortOption.PriceDesc:
                AddOrderByDesc(p => p.Price);
                break;
            default:
                AddOrderBy(p => p.Name);
                break;
        }
    }
}
