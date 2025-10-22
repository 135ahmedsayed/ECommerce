namespace Ecommerce.Domain.Contracts;
public interface IBasketRepository
{
    Task<bool> DeleteAsync(string id);
}
