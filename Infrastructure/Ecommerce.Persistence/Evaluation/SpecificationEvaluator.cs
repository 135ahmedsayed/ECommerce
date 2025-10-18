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

        //Include
        foreach (var include in specification.Includes)
            query = query.Include(include);
        
        return query;
    }
}

// include
// order
// filter
// pagination