﻿namespace Ecommerce.Domain.Entities.Basket;
public class CustomerBasket
{
    public string Id { get; set; } = default!;
    public ICollection<BasketItem> Items { get; set; } = [];
}
