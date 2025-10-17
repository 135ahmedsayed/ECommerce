using System.Linq.Expressions;

namespace Ecommerce.Domain.Contracts;
public interface ISpecification<TEntiy>
    where TEntiy : class
{
    //Include 
    ICollection<Expression<Func<TEntiy, object>>> Includes { get; }
}
