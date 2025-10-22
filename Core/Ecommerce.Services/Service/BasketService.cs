using AutoMapper;
using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities.Basket;
using Ecommerce.ServiceAbstraction;
using Ecommerce.Shared.DTOs.Baskets;

namespace Ecommerce.Services.Service;
public class BasketService(IBasketRepository basketRepository ,IMapper mapper)
    : IBasketService
{
    public async Task<CustomerBasketDTO> CreateOrUpdateAsync(CustomerBasketDTO basketDTO)
    {
        var basket = mapper.Map<CustomerBasket>(basketDTO);
        var updatedBasket = await basketRepository.CreateOrUpdateAsync(basket);
        return mapper.Map<CustomerBasketDTO>(updatedBasket);
    }

    public Task DeleteAsync(string id)
        => basketRepository.DeleteAsync(id);

    public async Task<CustomerBasketDTO> GetByIdAsync(string id)
    {
        var basket = await basketRepository.GetAsync(id);
        return mapper.Map<CustomerBasketDTO>(basket);
    }
}
