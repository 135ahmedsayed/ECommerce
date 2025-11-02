namespace Ecommerce.Domain.Contracts;
public interface IDbInitializer
{
    Task InitializeAsync();
    Task InitializeAuthDbAsync();
}
