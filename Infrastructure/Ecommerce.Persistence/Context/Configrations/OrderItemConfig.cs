global using Ecommerce.Domain.Entities.OrderEntities;

namespace Ecommerce.Persistence.Context.Configrations;
public class OrderItemConfig
    : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<OrderItem> builder)
    {
        builder.OwnsOne(i => i.Product, o => o.WithOwner());
        builder.Property(o => o.Price)
            .HasColumnType("decimal(10,2)");
    }
}
