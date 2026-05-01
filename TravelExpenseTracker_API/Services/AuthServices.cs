using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TravelExpenseTracker.Shared.Dtos;
using TravelExpenseTracker_API.Data;
using TravelExpenseTracker_API.Data.Entities;

namespace TravelExpenseTracker_API.Services;

public class AuthServices 
{
    private readonly DataContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly JwtService _jwtService;

    public AuthServices(DataContext context, IPasswordHasher<User> passwordHasher, JwtService jwtService)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task<ApiResult> RegisterAsync(RegisterDto dto)
    {
        // Validações
        if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
            return ApiResult.Fail("Email e senha são obrigatórios");

        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            return ApiResult.Fail("Email already exists");   // ← Adicionado return aqui!

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

        var jwt = _jwtService.GenetareJwt(user);

        return ApiResult<string>.Success(jwt);
    }

   
}
