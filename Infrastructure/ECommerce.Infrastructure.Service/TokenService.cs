using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ecommerce.Domain.Entities.Auth;
using ECommerce.Infrastructure.Service.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Infrastructure.Service;
public class TokenService(IOptions<JWTOptions> options) : ITokenService
{
    public string GetToken(ApplicationUser user, IList<string> roles)
    {
        var jwt = options.Value;
        List<Claim> claims =
        [
            new(JwtRegisteredClaimNames.Name , user.DisplayName),
            new(JwtRegisteredClaimNames.Email , user.Email),
        ];
        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(claims: claims,
            expires: DateTime.Now.AddMinutes(jwt.DurationInMinutes),
            signingCredentials: creds,
            audience: jwt.Audience,
            issuer:jwt.Issure
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
