using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TravelExpenseTracker.Models;

namespace TravelExpenseTracker.Services;

public class AuthService
{
    private const string TokenKey = "token";
    public string? Token { get; private set; }

    public LoggedInUser User { get; set; } = new(0, "", "");

    public void SetToken(string token)
    {
        Token = token;
        User = GetUserFromToken(token);
        Preferences.Default.Set(TokenKey, token);
    }

    public void RemoveToken()
    {
        Token = null;
        Preferences.Default.Remove(TokenKey);
    }

    public void Initialize()
    {
        if (Preferences.Default.ContainsKey(TokenKey))
        {
            var token = Preferences.Default.Get<string?>(TokenKey, null);
            if (string.IsNullOrWhiteSpace(token) || IsTokenExpired(token))
            {
                RemoveToken();
                return;
            }
        }
    }

    private JwtSecurityToken ParseToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);
        return jwtSecurityToken;
    }

    private LoggedInUser GetUserFromToken(string token)
    {
        var jwtSecurityToken = ParseToken(token);
        var id = Convert.ToInt32(jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
        var name = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        var email = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        return new LoggedInUser(id, name, email);
    }

    private bool IsTokenExpired(string token)
    {
        var jwtSecurityToken = ParseToken(token);
        var isExpired = jwtSecurityToken.ValidTo <= DateTime.UtcNow;
        return isExpired;
    }
}
