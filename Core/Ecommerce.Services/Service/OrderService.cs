using AutoMapper;
using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities.OrderEntities;
using Ecommerce.Domain.Entities.Products;
using Ecommerce.ServiceAbstraction;
using Ecommerce.ServiceAbstraction.Common;
using Ecommerce.Services.Specifications;
using Ecommerce.Shared.DTOs.UserOrder;
using ECommerce.ServicesAbstractions.Common;

namespace Ecommerce.Services.Service;
public class OrderService(IUnitOfWork unitOfWork,
    IMapper mapper,
    IBasketRepository basketRepository)
    : IOrderService
{
    public async Task<Result<OrderResponse>> CreateAsync(OrderRequest request, string email)
    {
        var basket = await basketRepository.GetAsync(request.basketId);
        if (basket == null)
            return Error.NotFound("Basket Not Found", $"Basket with Id {request.basketId} was not found");
        var method = await unitOfWork.GetRepostory<DeliveryMethod, int>()
            .GetByIDAsync(request.DeliveryMethodId);
        if (method == null)
            return Error.NotFound("Delivery Method Not Found", $"Delivery Method with Id {request.DeliveryMethodId} was not found");

        var productRepo = unitOfWork.GetRepostory<Product, int>();
        var ids = basket.Items.Select(i => i.Id).ToList();
        var products = (await productRepo.GetAllAsync(new GetProductByIdsSpecifications(ids))).ToDictionary(p => p.Id);

        var orderItems = new List<OrderItem>();
        var validationErrors = new List<Error>();
        foreach (var item in basket.Items)
        {
            if (!products.TryGetValue(item.Id, out Product? product))
            {
                validationErrors.Add(Error.Validation(""));
                continue;
            }
            var orderItem = new OrderItem
            {
                Product = new ProductInOrderItem
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    PictureUrl = product.PictureUrl
                },
                Price = product.Price,
                Quantity = item.Quantity
            };
            orderItems.Add(orderItem);
        }

        if (validationErrors.Any())
            return validationErrors;

        var subTotal = orderItems.Sum(i => i.Price * i.Quantity);
        var address = mapper.Map<OrderAddress>(request.Address);
        var order = new Order
        {
            UserEmail = email,
            deliveryMethod = method,
            Items = orderItems,
            Subtotal = subTotal,
            Address = address,
        };
        unitOfWork.GetRepostory<Order, Guid>().Add(order);
        await unitOfWork.SaveChangesAsync();
        
        return mapper.Map<OrderResponse>(order);
    }
}