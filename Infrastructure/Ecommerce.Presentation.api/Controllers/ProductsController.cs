using Ecommerce.ServiceAbstraction;
using Ecommerce.Shared.DTOs;
using Ecommerce.Shared.DTOs.Products;
using Microsoft.AspNetCore.Mvc;
namespace Ecommerce.Presentation.api.Controllers;
public class ProductsController(IProductService service)
    : ApiBaseController
{
    //Get All Products (with pagination , Search , order ,Filtration)
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<ProductResponse>>> GetProducts([FromQuery] ProductQueryParameter parameters,CancellationToken cancellationToken = default)
    {
        var products = await service.GetProductsAsync(parameters,cancellationToken);
        return Ok(products);
    }
    //Get By Id
    [HttpGet("{Id}")]
    public async Task<ActionResult<ProductResponse>> GetById(int Id ,CancellationToken cancellationToken = default)
    {
        var products = await service.GetByIdAsync(Id ,cancellationToken);
        return Ok(products);
    }
    //GetBrands
    [HttpGet("Brands")]
    public async Task<ActionResult<IEnumerable<BrandResponse>>> GetBrands(CancellationToken cancellationToken = default)
    {
        var products = await service.GetBrandsAsync(cancellationToken);
        return Ok(products);
    }
    //GetTypes
    [HttpGet("Types")]
    public async Task<ActionResult<IEnumerable<TypeResponse>>> GetTypes(CancellationToken cancellationToken = default)
    {
        var products = await service.GetTypesAsync(cancellationToken);
        return Ok(products);
    }
}
