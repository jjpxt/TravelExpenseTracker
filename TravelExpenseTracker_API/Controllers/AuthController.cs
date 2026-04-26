using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelExpenseTracker.Shared.Dtos;
using TravelExpenseTracker_API.Services;

namespace TravelExpenseTracker_API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthServices _authServices;

    public AuthController(AuthServices authServices)
    {
        _authServices = authServices;
    }

    [HttpPost("register")]
    public async Task<ApiResult> Register(RegisterDto dto) =>
        await _authServices.RegisterAsync(dto);

    [HttpPost("login")]
    public async Task<ApiResult<string>> Login(LoginDto dto) =>
        await _authServices.LoginAsync(dto);
}
