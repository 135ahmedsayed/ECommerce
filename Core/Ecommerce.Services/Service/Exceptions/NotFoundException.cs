namespace Ecommerce.Services.Service.Exceptions;
// Not Found Exception Base Class
public abstract class NotFoundException(string Message) : Exception(Message);

public sealed class ProductNotFoundException(int Id)
    : NotFoundException($"Product with Id {Id} was Not Found");

public sealed class BasketNotFoundException(string Id)
    : NotFoundException($"Product with Id {Id} was Not Found");