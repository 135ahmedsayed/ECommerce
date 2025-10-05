namespace Ecommerce.Domain.Entities.Products;
public abstract class Entity<TKey>
{
    public TKey Id { get; set; } = default!;


}
