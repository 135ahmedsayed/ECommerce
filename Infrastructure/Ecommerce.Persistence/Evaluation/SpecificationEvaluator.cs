namespace Ecommerce.Persistence.Evaluation;
internal static class SpecificationEvaluator
{
    public static IQueryable<TEntity> ApplySpecification<TEntity>(this IQueryable<TEntity> inputQuery
        ,ISpecification<TEntity> specification)
             where TEntity : class
    {
        var query = inputQuery;


        //Where
        if (specification.Criteria != null)
            query = query.Where(specification.Criteria);
        //___________________________________
        //Include
        foreach (var include in specification.Includes)
            query = query.Include(include);
        //___________________________________
        //Order(Sorting)
        if (specification.OrderBy != null)
            query = query.OrderBy(specification.OrderBy);
        else if (specification.OrderByDesc != null)
            query = query.OrderByDescending(specification.OrderByDesc);
        //___________________________________
        //pagination
        if (specification.IsPaginated)
            query = query.Skip(specification.Skip).Take(specification.Take);
        return query;
    }
}

// include
// order
// filter
// pagination