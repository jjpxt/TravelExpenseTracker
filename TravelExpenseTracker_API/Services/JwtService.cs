using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TravelExpenseTracker_API.Data.Entities;

namespace TravelExpenseTracker_API.Services;

public class JwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenetareJwt(User user)
    {
        Claim[] claims = [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypes.Name, user.Name),
            new (ClaimTypes.Email, user.Email),
            ];

        var key = _configuration.GetValue<string>("Jwt:SecureKey");
        var keyBiteArray = Encoding.UTF8.GetBytes(key);
        var securityKey = new SymmetricSecurityKey(keyBiteArray);

        var signinCreds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _configuration.GetValue<string>("Jwt:Issuer"),
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: signinCreds
            );

        var jwt = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return jwt;
    }
}
