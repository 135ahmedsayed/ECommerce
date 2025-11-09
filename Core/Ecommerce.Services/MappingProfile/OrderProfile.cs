using AutoMapper;
using Ecommerce.Domain.Entities.OrderEntities;
using Ecommerce.Domain.Entities.Products;
using Ecommerce.Shared.DTOs.Products;
using Ecommerce.Shared.DTOs.UserOrder;
using Ecommerce.Shared.DTOs.Users;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Services.MappingProfile;
public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderResponse>()
            .ForMember(d => d.deliveryMethod ,
            o => o.MapFrom(s => s.deliveryMethod.ShortName))
            .ForMember(d => d.DeliveryMethodCost ,
            o => o.MapFrom(s => s.deliveryMethod.Price))
            .ForMember(d => d.Total ,
            o => o.MapFrom(s => s.deliveryMethod.Price + s.Subtotal));

        CreateMap<OrderAddress, AddressDTO>().ReverseMap();

        CreateMap<OrderItem, OrderItemDTO>()
            .ForMember(d => d.ProductId,
            o => o.MapFrom(s => s.Product.ProductId))
            .ForMember(d => d.Name,
            o => o.MapFrom(s => s.Product.Name))
            .ForMember(d => d.PictureUrl,
            o => o.MapFrom<OrderPictureUrlResolver>());
    }
}

internal class OrderPictureUrlResolver(IConfiguration configuration)
    : IValueResolver<OrderItem, OrderItemDTO, string>
{
    public string? Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
    {
        if (string.IsNullOrWhiteSpace(source.Product.PictureUrl))
            return null;
        return $"{configuration["BaseUrl"]}{source.Product.PictureUrl}";
    }
}
