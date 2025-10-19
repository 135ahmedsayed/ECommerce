using Ecommerce.Shared.DTOs;
using Ecommerce.Shared.DTOs.Products;

namespace Ecommerce.ServiceAbstraction;
public interface IProductService
{
    Task<ProductResponse?> GetByIdAsync(int id , CancellationToken cancellationToken = default);
    Task<PaginatedResult<ProductResponse>> GetProductsAsync(ProductQueryParameter parameters,CancellationToken cancellationToken = default);
    Task<IEnumerable<TypeResponse>> GetTypesAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<BrandResponse>> GetBrandsAsync(CancellationToken cancellationToken = default);
    
}
