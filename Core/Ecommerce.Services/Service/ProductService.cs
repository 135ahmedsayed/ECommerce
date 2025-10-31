using AutoMapper;
using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities.Products;
using Ecommerce.ServiceAbstraction;
using Ecommerce.ServiceAbstraction.Common;
using Ecommerce.Services.Service.Exceptions;
using Ecommerce.Services.Specifications;
using Ecommerce.Shared.DTOs;
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

    public async Task<Result<ProductResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var products = await unitOfWork.GetRepostory<Product, int>()
            .GetAsync(new ProductWithBrandTypeSpecification(id),cancellationToken)
            ?? throw new ProductNotFoundException(id); // Call Base Class Not Found Exception
                                                       
        //if(products == null)
        //    return Result<ProductResponse>.Fail(Error.NotFound());
        //return Result<ProductResponse>.Ok(mapper.Map<ProductResponse>(products));
        // OR implicit
        if (products == null)
            return Error.NotFound();
        return mapper.Map<ProductResponse>(products);


    }

    public async Task<PaginatedResult<ProductResponse>> GetProductsAsync(ProductQueryParameter parameters, CancellationToken cancellationToken = default)
    {
        var spec = new ProductWithBrandTypeSpecification(parameters);
        var products = await unitOfWork.GetRepostory<Product, int>()
            .GetAllAsync(spec,cancellationToken);
        var totalCount = await unitOfWork.GetRepostory<Product, int>()
            .CountAsync(new ProductCountSpecification(parameters), cancellationToken);
        var Products = mapper.Map<IEnumerable<ProductResponse>>(products);
        return new(parameters.PageIndex, Products.Count(), totalCount, Products);
    }

    public async Task<IEnumerable<TypeResponse>> GetTypesAsync(CancellationToken cancellationToken = default)
    {
        var Types = await unitOfWork.GetRepostory<ProductType, int>()
            .GetAllAsync(cancellationToken);
        return mapper.Map<IEnumerable<TypeResponse>>(Types);
    }
}
