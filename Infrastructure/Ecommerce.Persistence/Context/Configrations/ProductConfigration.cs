
namespace Ecommerce.Persistence.Context.Configrations;
internal class ProductConfigration : IEntityTypeConfiguration<Product>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
    {
        builder.Property(b => b.Name)
               .HasMaxLength(256)
               .HasColumnType("VarChar");
        builder.Property(b => b.PictureUrl)
               .HasMaxLength(256)
               .HasColumnType("VarChar");
        builder.Property(b => b.Description)
               .HasMaxLength(500)
               .HasColumnType("VarChar");
        builder.Property(b => b.Price)
               .HasColumnType("decimal(10,2)");

        builder.HasOne(b => b.ProductBrand)
            .WithMany()
            .HasForeignKey(b => b.BrandId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(b => b.ProductType)
            .WithMany()
            .HasForeignKey(b => b.TypeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
