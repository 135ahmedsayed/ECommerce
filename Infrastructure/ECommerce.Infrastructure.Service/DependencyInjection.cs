using ECommerce.Infrastructure.Service.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure.Service;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add infrastructure services here

        services.AddScoped<ITokenService, TokenService>();
        return services;
    }
}
