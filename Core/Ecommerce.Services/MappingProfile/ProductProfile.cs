using AutoMapper;
using Ecommerce.Domain.Entities.Products;
using Ecommerce.Shared.DTOs.Products;

namespace Ecommerce.Services.MappingProfile;
public class ProductProfile :Profile
{
    public ProductProfile()
    {
        // Mapping configurations
        CreateMap<Product, ProductResponse>()
            .ForMember(d => d.Brand,
            o => o.MapFrom(s => s.ProductBrand.Name));
        CreateMap<Product, ProductResponse>()
            .ForMember(d => d.Type,
            o => o.MapFrom(s => s.ProductType.Name));

        CreateMap<ProductBrand, BrandResponse>();
        CreateMap<ProductType, TypeResponse>();
    }

}
