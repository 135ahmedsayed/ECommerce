using AutoMapper;
using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities.Products;
using Ecommerce.ServiceAbstraction;
using Ecommerce.Shared.DTOs.Products;

namespace Ecommerce.Services.Service;
public class ProductService(IUnitOfWork unitOfWork , IMapper mapper) : IProductService
{
    public async Task<IEnumerable<BrandResponse>> GetBrandsAsync(CancellationToken cancellationToken = default)
    {
        var brands = await unitOfWork.GetRepostory<ProductBrand, int>()
            .GetAllAsync(cancellationToken);
        return mapper.Map<IEnumerable<BrandResponse>>(brands);
    }

    public async Task<ProductResponse?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var products = await unitOfWork.GetRepostory<Product, int>()
            .GetByIDAsync(id,cancellationToken);
        return mapper.Map<ProductResponse>(products);
    }

    public async Task<IEnumerable<ProductResponse>> GetProductsAsync(CancellationToken cancellationToken = default)
    {
        var products = await unitOfWork.GetRepostory<Product, int>()
            .GetAllAsync(cancellationToken);
        return mapper.Map<IEnumerable<ProductResponse>>(products);
    }

    public async Task<IEnumerable<TypeResponse>> GetTypesAsync(CancellationToken cancellationToken = default)
    {
        var Types = await unitOfWork.GetRepostory<ProductType, int>()
            .GetAllAsync(cancellationToken);
        return mapper.Map<IEnumerable<TypeResponse>>(Types);
    }
}
