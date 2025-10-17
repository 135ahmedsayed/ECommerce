using System.Linq.Expressions;
using Ecommerce.Domain.Contracts;

namespace Ecommerce.Services.Specifications;
internal abstract class BaseSpecification<TEntity> : ISpecification<TEntity>
    where TEntity : class
{
    // Include(Implement)
    public ICollection<Expression<Func<TEntity, object>>> Includes { get; private set; } = [];

    protected void AddInclude(Expression<Func<TEntity, object>> expression)
    {
        Includes.Add(expression);
    }
}
