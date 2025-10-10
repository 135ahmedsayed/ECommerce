using Ecommerce.ServiceAbstraction;
using Ecommerce.Shared.DTOs.Products;

namespace Ecommerce.Services.Service;
public class ProductService : IProductService
{
    public Task<IEnumerable<BrandResponse>> GetBrandsAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<ProductResponse?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductResponse>> GetProductsAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TypeResponse>> GetTypesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
