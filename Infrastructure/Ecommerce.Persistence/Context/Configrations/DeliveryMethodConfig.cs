using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Context.Configrations;
public class DeliveryMethodConfig
    : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.Property(dm => dm.Price)
            .HasColumnType("decimal(10,2)");
        builder.Property(dm => dm.ShortName)
            .HasColumnType("VarChar")
            .HasMaxLength(64);
        builder.Property(dm => dm.DeliveryTime)
            .HasColumnType("VarChar")
            .HasMaxLength(128);
        builder.Property(dm => dm.Description)
            .HasColumnType("VarChar")
            .HasMaxLength(128);
    }
}
