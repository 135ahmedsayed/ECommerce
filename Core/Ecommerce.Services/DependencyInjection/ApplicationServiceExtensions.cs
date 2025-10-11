using System.Reflection;
using Ecommerce.ServiceAbstraction;
using Ecommerce.Services.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Services.DependencyInjection;
public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
    {
        Services.AddScoped<IProductService, ProductService>();
        Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return Services;
    }
}
