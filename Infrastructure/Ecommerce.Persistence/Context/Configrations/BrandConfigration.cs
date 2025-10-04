using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Context.Configrations;
internal class BrandConfigration : IEntityTypeConfiguration<ProductBrand>
{
    public void Configure(EntityTypeBuilder<ProductBrand> builder)
    {
         builder.Property(b => b.Name)
               .HasMaxLength(256)
               .HasColumnType("VarChar");
    }
}
