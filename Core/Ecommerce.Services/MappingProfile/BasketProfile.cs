using AutoMapper;
using Ecommerce.Domain.Entities.Basket;
using Ecommerce.Shared.DTOs.Baskets;

namespace Ecommerce.Services.MappingProfile;
public class BasketProfile : Profile
{
    public BasketProfile()
    {
        CreateMap<BasketItem, BasketItemDTO>()
            .ReverseMap();
        CreateMap<CustomerBasket, CustomerBasketDTO>()
            .ReverseMap();
    }
}
