using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Domain.Entities.Auth;
public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? DisplayName { get; set; }
    public Address Address { get; set; }
}
