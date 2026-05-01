using Refit;
using TravelExpenseTracker.Shared.Dtos;

namespace TravelExpenseTracker.Apis;

public interface IAuthApi
{
    [Post("/api/auth/register")]
    Task<ApiResult> RegisterAsync(RegisterDto dto);

    [Post("/api/auth/login")]
    Task<ApiResult<string>> LoginAsync(LoginDto dto);
}
