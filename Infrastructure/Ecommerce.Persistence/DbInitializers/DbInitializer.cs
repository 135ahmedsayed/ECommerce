using Microsoft.VisualBasic;

namespace Ecommerce.Persistence.DbInitializers;
internal class DbInitializer(StoreDbContext dbContext)
    : IDbInitializer
{
    public async Task InitializeAsync()
    {
        try
        {
            if((await dbContext.Database.GetPendingMigrationsAsync()).Any())
            await dbContext.Database.MigrateAsync();

            if (!dbContext.ProductBrands.Any())
            {
                //Read from json
                var BrandDate = await File.ReadAllTextAsync(@"..\Infrastructure\Ecommerce.Persistence\Context\DataSeed\brands.json");
                //Deserialize Convert From json to C#
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandDate , options);
                //Save to db 
                if (brands is not null && brands.Any())
                {
                    await dbContext.ProductBrands.AddRangeAsync(brands);
                }
                await dbContext.SaveChangesAsync();
            }
            if (!dbContext.ProductTypes.Any())
            {
                //Read from json
                var typeDate = await File.ReadAllTextAsync(@"..\Infrastructure\Ecommerce.Persistence\Context\DataSeed\types.json");
                //Deserialize Convert From json to C#
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var Types = JsonSerializer.Deserialize<List<ProductType>>(typeDate, options);
                //Save to db 
                if (Types is not null && Types.Any())
                {
                    await dbContext.ProductTypes.AddRangeAsync(Types);
                }
                await dbContext.SaveChangesAsync();
            }
            if (!dbContext.Products.Any())
            {
                //Read from json
                var ProductDate = await File.ReadAllTextAsync(@"..\Infrastructure\Ecommerce.Persistence\Context\DataSeed\products.json");
                //Deserialize Convert From json to C#
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var products = JsonSerializer.Deserialize<List<Product>>(ProductDate, options);
                //Save to db 
                if (products is not null && products.Any())
                {
                    await dbContext.Products.AddRangeAsync(products);
                }
                await dbContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}