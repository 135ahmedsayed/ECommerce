using Ecommerce.Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Ecommerce.Persistence.AuthContext;
internal class AuthDbContext(DbContextOptions<AuthDbContext> options)
    :IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Address> Addresses { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}
