using Ecommerce.Domain.Entities.OrderEntities;

namespace Ecommerce.Services.Specifications;
internal class OrderByIdAndEmailSpecification
    : BaseSpecification<Order>
{
    public OrderByIdAndEmailSpecification(string email , Guid Id)
        : base(o => o.Id == Id && o.UserEmail == email)
    {
        AddInclude(o => o.Items);
        AddInclude(o => o.deliveryMethod!);
    }
}
