using System.Linq.Expressions;

namespace Ecommerce.Domain.Contracts;
public interface ISpecification<TEntiy>
    where TEntiy : class
{
    //Criteria
    Expression<Func<TEntiy, bool>> Criteria { get; }
    //Include 
    ICollection<Expression<Func<TEntiy, object>>> Includes { get; }

}
