namespace Ecommerce.Domain.Entities.Auth;
public class Address
{
#nullable disable
    public ApplicationUser user { get; set; }
    public string userId { get; set; }

    public int id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string Country { get; set; }
}
