using Ecommerce.Persistence.Evaluation;

namespace Ecommerce.Persistence.Repositories;
public class Repository<TEntity, TKey>(ApplicationDbContext dbContext)
    : IRepostory<TEntity, TKey>
    where TEntity : Entity<TKey>
{
    //private readonly DbSet<TEntity> dbSet = dbContext.Set<TEntity>();
    public void Add(TEntity entity)
        => dbContext.Set<TEntity>().Add(entity);

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => await dbContext.Set<TEntity>().ToListAsync(cancellationToken);


    public async Task<TEntity?> GetByIDAsync(TKey id, CancellationToken cancellationToken = default)
        => await dbContext.Set<TEntity>().FindAsync([id], cancellationToken);

    public void Remove(TEntity entity)
        => dbContext.Set<TEntity>().Remove(entity);

    public void Update(TEntity entity)
        => dbContext.Set<TEntity>().Update(entity);


    //include 
    public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<TEntity>().ApplySpecification(specification)
            .ToListAsync(cancellationToken);
    }
    //where
    public async Task<TEntity?> GetAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<TEntity>().ApplySpecification(specification)
            .FirstOrDefaultAsync(cancellationToken);
    }
    //count(Pagination)
    public async Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<TEntity>().ApplySpecification(specification)
            .CountAsync(cancellationToken);
    }
}
