using Ecommerce.Domain.Entities.Auth;
using Ecommerce.Persistence.AuthContext;
using Ecommerce.Persistence.BasketRepo;
using Ecommerce.Persistence.Context;
using Ecommerce.Persistence.DbInitializers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Ecommerce.Persistence.DependancyInjection;
public static class PersistenceServiceExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection Services,
        IConfiguration configuration)
    {
        //AuthContext
        Services.AddDbContext<AuthDbContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("AuthConnection")!);
        });
        //basket
        Services.AddScoped<ICashService, ServiceCash>();
        Services.AddScoped<IBasketRepository, BasketRepository>();
        //RedisConnection
        Services.AddSingleton<IConnectionMultiplexer>(cfg =>
        {
            return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")!);
        });

        //____________________________________________________________
        Services.AddDbContext<StoreDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("SQLConnection");
            options.UseSqlServer(connectionString);
        });
        // Configure Repositories and UnitOfWork Pattern
        Services.AddScoped<IUnitOfWork,UnitOfWork>();
        Services.AddScoped<IDbInitializer, DbInitializer>();

        //Identity Configuration
        ConfigrationIdentity(Services, configuration);
        return Services;
    }


    private static void ConfigrationIdentity(IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
        })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AuthDbContext>();
    }
}
