using Ecommerce.Persistence.Context;
using Ecommerce.Persistence.DbInitializers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Ecommerce.Persistence.DependancyInjection;
public static class PersistenceServiceExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection Services,
        IConfiguration configuration)
    {
        //RedisConnection
        Services.AddSingleton<IConnectionMultiplexer>(cfg =>
        {
            return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")!);
        });

        //____________________________________________________________
        Services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("SQLConnection");
            options.UseSqlServer(connectionString);
        });
        // Configure Repositories and UnitOfWork Pattern
        Services.AddScoped<IUnitOfWork,UnitOfWork>();
        Services.AddScoped<IDbInitializer, DbInitializer>();
        return Services;
    }
}
