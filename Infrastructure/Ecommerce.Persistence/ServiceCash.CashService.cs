namespace Ecommerce.Persistence;

public class ServiceCash(IConnectionMultiplexer multiplexer)
    : ICashService
{
    private readonly IDatabase _database=multiplexer.GetDatabase();
    public async Task<string?> GetAsync(string key)
    {
        return await _database.StringGetAsync(key);
    }

    public async Task SetAsync(string key, object value, TimeSpan TTL)
    {
        var json = JsonSerializer.Serialize(value);
        await _database.StringSetAsync(key, json, TTL);
    }
}
