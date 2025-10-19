using System.Linq.Expressions;
using Ecommerce.Domain.Entities.Products;
using Ecommerce.Shared.DTOs.Products;

namespace Ecommerce.Services.Specifications;
internal sealed class ProductCountSpecification(ProductQueryParameter parameters) : BaseSpecification<Product>(CreateCriteira(parameters))
{
    private static Expression<Func<Product, bool>> CreateCriteira(ProductQueryParameter parameters)
    {
        return p => (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId.Value)
                    && (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId.Value)
                    && (string.IsNullOrWhiteSpace(parameters.Search) || p.Name.Contains(parameters.Search!));
    }
}
