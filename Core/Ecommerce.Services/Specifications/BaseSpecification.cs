using System.Linq.Expressions;
using Ecommerce.Domain.Contracts;

namespace Ecommerce.Services.Specifications;
internal abstract class BaseSpecification<TEntity> : ISpecification<TEntity>
    where TEntity : class
{
    // Criteria(Implement)
    public Expression<Func<TEntity, bool>> Criteria { get; private set; }
    protected BaseSpecification(Expression<Func<TEntity, bool>> criteria)
    {
        Criteria = criteria;
    }



    // Include(Implement)
    public ICollection<Expression<Func<TEntity, object>>> Includes { get; private set; } = [];


    protected void AddInclude(Expression<Func<TEntity, object>> expression)
    {
        Includes.Add(expression);
    }

    // Sorting Implement
    public Expression<Func<TEntity, object>> OrderBy { get; private set; }

    public Expression<Func<TEntity, object>> OrderByDesc { get; private set; }


    protected void AddOrderBy(Expression<Func<TEntity, object>> expression)
        => OrderBy = expression;
    protected void AddOrderByDesc(Expression<Func<TEntity, object>> expression)
        => OrderByDesc = expression;

    // Pagination (Optional) Implement
    public int Skip { get; private set; }

    public int Take {get; private set; }

    public bool IsPaginated {get; private set; }

    protected void ApplyPagination(int pageSize, int pageIndex)
    {
        Skip = (pageIndex - 1) * pageSize;
        Take = pageSize;
        IsPaginated = true;
    }

}
