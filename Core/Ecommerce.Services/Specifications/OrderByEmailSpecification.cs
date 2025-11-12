using Ecommerce.Domain.Entities.OrderEntities;

namespace Ecommerce.Services.Specifications;
internal class OrderByEmailSpecification
    : BaseSpecification<Order>
{
    public OrderByEmailSpecification(string email)
        : base(o => o.UserEmail == email)
    {
        AddInclude(o => o.Items);
        AddInclude(o => o.deliveryMethod!);
        AddOrderBy(o => o.OrderDate);
    }
}
