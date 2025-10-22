using Ecommerce.Domain.Entities.Basket;
using StackExchange.Redis;

namespace Ecommerce.Persistence.BasketRepo;
internal class BasketRepository(IConnectionMultiplexer multiplexer) : IBasketRepository
{
    private readonly IDatabase _database = multiplexer.GetDatabase();
    public async Task<CustomerBasket> CreateOrUpdateAsync(CustomerBasket basket, TimeSpan? TTL = null /* segniture*/)
    {
        var json = JsonSerializer.Serialize(basket); // redis value = json
        await _database.StringSetAsync(basket.Id, json, TTL??TimeSpan.FromDays(7)); // function Call
        return (await GetAsync(basket.Id))!;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _database.KeyDeleteAsync(id);
    }

    public async Task<CustomerBasket?> GetAsync(string id)
    {
        var json =await _database.StringGetAsync(id);
        //null
        if (json.IsNullOrEmpty)
            return null;
        //!null = CustomerBasket (Deserlization)
        return JsonSerializer.Deserialize<CustomerBasket>(json!);
    }
}
