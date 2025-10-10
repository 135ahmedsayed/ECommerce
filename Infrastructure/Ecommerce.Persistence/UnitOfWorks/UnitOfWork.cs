namespace Ecommerce.Persistence.UnitOfWork;
public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
{
    private readonly Dictionary<string, object> _repositories = [];
    public IRepostory<TEntity, Tkey> GetRepostory<TEntity, Tkey>() where TEntity : Entity<Tkey>
    {
        var typeName = typeof(TEntity).Name; // Product Key
        if (_repositories.TryGetValue(typeName , out object? value))
            return (IRepostory<TEntity, Tkey>)value; // cast to IRepostory<Product, int>
        var repo = new Repository<TEntity, Tkey>(dbContext);
        _repositories.Add(typeName, repo);
        return repo;
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await dbContext.SaveChangesAsync(cancellationToken);
}
