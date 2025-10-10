using Ecommerce.Domain.Entities.Products;

namespace Ecommerce.Domain.Contracts;
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    IRepostory<TEntity, Tkey> GetRepostory<TEntity,Tkey>()
            where TEntity : Entity<Tkey>;
}
