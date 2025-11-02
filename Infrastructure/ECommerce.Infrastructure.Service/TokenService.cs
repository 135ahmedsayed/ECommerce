using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ecommerce.Domain.Entities.Auth;
using ECommerce.Infrastructure.Service.Contracts;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Infrastructure.Service;
public class TokenService : ITokenService
{
    public string GetToken(ApplicationUser user, IList<string> roles)
    {
        List<Claim> claims =
        [
            new(JwtRegisteredClaimNames.Name , user.DisplayName),
            new(JwtRegisteredClaimNames.Email , user.Email),
        ];
        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super secret keysuper secret keysuper secret keysuper secret key"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds,
            issuer:"My-Api-Project",
            audience: "My-Api-Project");

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
