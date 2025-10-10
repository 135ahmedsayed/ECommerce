
using Ecommerce.Persistence.Context;
using Ecommerce.Persistence.DbInitializers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Persistence.DependancyInjection;
public static class PersistenceServiceExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection Services,
        IConfiguration configuration)
    {
        Services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("SQLConnection");
            options.UseSqlServer(connectionString);
        });
        Services.AddScoped<IUnitOfWork,UnitOfWork>();
        Services.AddScoped<IDbInitializer, DbInitializer>();
        return Services;
    }
}
