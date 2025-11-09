using System.Reflection;
using Ecommerce.ServiceAbstraction;
using Ecommerce.Services.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Services.DependencyInjection;
public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
    {
        Services.AddScoped<IBasketService, BasketService>();
        Services.AddScoped<IOrderService, OrderService>();
        Services.AddScoped<IProductService, ProductService>();
        Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        Services.AddScoped<IUserService, UserService>();
        Services.AddScoped<IAuthService, AuthService>();
        return Services;
    }
}
