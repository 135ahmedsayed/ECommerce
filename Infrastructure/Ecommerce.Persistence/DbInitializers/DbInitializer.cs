using Ecommerce.Domain.Entities.Auth;
using Ecommerce.Domain.Entities.OrderEntities;
using Ecommerce.Persistence.AuthContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace Ecommerce.Persistence.DbInitializers;
internal class DbInitializer(StoreDbContext dbContext ,
    AuthDbContext authDbContext ,
    RoleManager<IdentityRole> roleManager , 
    UserManager<ApplicationUser> userManager ,
    ILogger<DbInitializer> logger)
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
            if (!dbContext.DeliveryMethods.Any())
            {
                //Read from json
                var ProductDate = await File.ReadAllTextAsync(@"..\Infrastructure\Ecommerce.Persistence\Context\DataSeed\delivery.json");
                //Deserialize Convert From json to C#
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var delivery = JsonSerializer.Deserialize<List<DeliveryMethod>>(ProductDate, options);
                //Save to db 
                if (delivery is not null && delivery.Any())
                {
                    await dbContext.DeliveryMethods.AddRangeAsync(delivery);
                }
                await dbContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task InitializeAuthDbAsync()
    {
        await authDbContext.Database.MigrateAsync();


        if (!roleManager.Roles.Any())
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
        }

        if (!userManager.Users.Any())
        {
            var superAdminUser = new ApplicationUser
            {
                DisplayName = "Super Admin",
                Email = "SuperAdmin@gmail.com",
                UserName = "SuperAdmin",
                PhoneNumber = "0123465789"
            };

            var adminUser = new ApplicationUser
            {
                DisplayName = "Admin",
                Email = "Admin@gmail.com",
                UserName = "Admin",
                PhoneNumber = "0123465789"
            };

            await userManager.CreateAsync(superAdminUser, "Passw0rd");
            await userManager.CreateAsync(adminUser, "Passw0rd");


            await userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}