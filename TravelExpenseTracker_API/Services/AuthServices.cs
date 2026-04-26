using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TravelExpenseTracker.Shared.Dtos;
using TravelExpenseTracker_API.Data;
using TravelExpenseTracker_API.Data.Entities;

namespace TravelExpenseTracker_API.Services;

public class AuthServices 
{
    private readonly DataContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IConfiguration _configuration;

    public AuthServices(DataContext context, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
    }

    public async Task<ApiResult> RegisterAsync(RegisterDto dto)
    {
        if(await _context.Users.AnyAsync(u => u.Email == dto.Email)) 
        {
            ApiResult.Fail("Email already exists");
        }

        var user = new User
        {
            Email = dto.Email,
            Name = dto.Name,
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

        _context.Add(user);
        await _context.SaveChangesAsync();
        return ApiResult.Success();
    }

    public async Task<ApiResult<string>> LoginAsync(LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user is null)
        {
            return ApiResult<string>.Fail("User does not exist");
        }

        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if(passwordVerificationResult != PasswordVerificationResult.Success)
        {
            return ApiResult<string>.Fail("Incorrect Password");
        }

        var jwt = GenetareJwt(user);

        return ApiResult<string>.Success(jwt);
    }

    private string GenetareJwt(User user)
    {
        Claim[] claims = [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypes.Name, user.Name),
            new (ClaimTypes.Email, user.Email),
            ];

        var key = _configuration.GetValue<string>("Jwt:SecureKey");
        var keyBiteArray = Encoding.UTF8.GetBytes(key);
        var securityKey = new SymmetricSecurityKey(keyBiteArray);

        var signinCreds = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

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
