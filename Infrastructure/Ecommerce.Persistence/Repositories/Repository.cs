namespace Ecommerce.Persistence.Repositories;
public class Repository<TEntity, TKey>(ApplicationDbContext dbContext)
    : IRepostory<TEntity, TKey>
    where TEntity : Entity<TKey>
{
    public void Add(TEntity entity)
        => dbContext.Set<TEntity>().Add(entity);

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => await dbContext.Set<TEntity>().ToListAsync(cancellationToken);

    public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        => await dbContext.Set<TEntity>().ToListAsync(cancellationToken);

    public async Task<TEntity?> GetByIDAsync(TKey id, CancellationToken cancellationToken = default)
        => await dbContext.Set<TEntity>().FindAsync([id], cancellationToken);

    public void Remove(TEntity entity)
        => dbContext.Set<TEntity>().Remove(entity);

    public void Update(TEntity entity)
        => dbContext.Set<TEntity>().Update(entity);
}
