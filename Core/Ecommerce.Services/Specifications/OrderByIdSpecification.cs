using Ecommerce.Domain.Entities.OrderEntities;

namespace Ecommerce.Services.Specifications;
internal class OrderByIdSpecification
    : BaseSpecification<Order>
{
    public OrderByIdSpecification(Guid Id)
        : base(o => o.Id == Id)
    {
        AddInclude(o => o.Items);
        AddInclude(o => o.deliveryMethod!);
    }
}
