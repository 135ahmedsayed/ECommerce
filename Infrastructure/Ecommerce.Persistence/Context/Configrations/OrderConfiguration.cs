global using Order = Ecommerce.Domain.Entities.OrderEntities.Order;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Context.Configrations;
public class OrderConfiguration
    : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasMany(x => x.Items)
            .WithOne()
            .HasForeignKey(x =>x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.deliveryMethod)
            .WithMany()
            .HasForeignKey(f => f.DeliveryMethodId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.OwnsOne(o => o.Address, x => x.WithOwner());

        builder.HasIndex(o => o.UserEmail);

        builder.Property(o => o.Subtotal)
            .HasColumnType("decimal(10,2)");

        builder.Property(o => o.UserEmail)
            .HasColumnType("VarChar")
            .HasMaxLength(128);
        
        builder.Property(o => o.PaymentIntentId)
            .HasColumnType("VarChar")
            .HasMaxLength(128);

        builder.Property(o => o.Status)
            .HasConversion<string>();


    }
}
