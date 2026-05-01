namespace TravelExpenseTracker.Services;

public class AuthService
{
    public string? Token { get; private set; }

    private const string TokenKey = "token";

    public void SetToken(string token) => Preferences.Default.Set(TokenKey, token);

    public void RemoveToken() => Preferences.Default.Remove(TokenKey);
}
