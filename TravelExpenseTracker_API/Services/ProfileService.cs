using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TravelExpenseTracker.Shared.Dtos;
using TravelExpenseTracker_API.Data;
using TravelExpenseTracker_API.Data.Entities;

namespace TravelExpenseTracker_API.Services;

public class ProfileService
{
    private readonly DataContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly JwtService _jwtService;

    public ProfileService(DataContext context, IPasswordHasher<User> passwordHasher, JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
        _passwordHasher = passwordHasher;
    }

    public async Task<ApiResult<string>> UpdateNameAsync(string name, int userId)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user is null)
                return ApiResult<string>.Fail("Invalid request");

            user.Name = name;
            await _context.SaveChangesAsync();

            var newJwt = _jwtService.GenetareJwt(user);

            return ApiResult<string>.Success(newJwt);
        }
        catch (Exception ex)
        {
            return ApiResult<string>.Fail(ex.Message);
        }
    }

    public async Task<ApiResult> ChangePasswordAsync(string newPassword, int userId)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user is null)
                return ApiResult.Fail("Invalid request");

            user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);

            await _context.SaveChangesAsync();

            return ApiResult.Success();
        }
        catch (Exception ex)
        {
            return ApiResult.Fail(ex.Message);
        }
    }
}
