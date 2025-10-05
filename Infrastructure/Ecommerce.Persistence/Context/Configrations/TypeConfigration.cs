using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Context.Configrations;
internal class TypeConfigration : IEntityTypeConfiguration<ProductType>
{
    public void Configure(EntityTypeBuilder<ProductType> builder)
    {
        builder.Property(b => b.Name)
               .HasMaxLength(256)
               .HasColumnType("VarChar");
    }
}
