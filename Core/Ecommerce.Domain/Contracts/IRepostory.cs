using Ecommerce.Domain.Entities.Products;

namespace Ecommerce.Domain.Contracts;
public interface IRepostory<TEntity , TKey>
    where TEntity : Entity<TKey> 
{
    public void Add(TEntity entity);
    public void Update(TEntity entity);
    public void Remove(TEntity entity);

    Task<TEntity?> GetByIDAsync(TKey id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    //Specification pattern
    Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification,CancellationToken cancellationToken = default);
    Task<TEntity?> GetAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

    Task<int> CountAsync(ISpecification<TEntity> specification,CancellationToken cancellationToken = default);
}
