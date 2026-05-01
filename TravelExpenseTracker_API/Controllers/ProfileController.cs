using Microsoft.AspNetCore.Mvc;
using TravelExpenseTracker.Shared.Dtos;
using TravelExpenseTracker_API.Services;

namespace TravelExpenseTracker_API.Controllers;

[Route("api/me")]
[ApiController]
public class ProfileController : AppBaseController
{
    private readonly ProfileService _profileService;

    public ProfileController(ProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpPost("update-name")]
    public async Task<ApiResult<string>> UpdateName(SingleValueDto<string> dto)
    {
        var newName = dto.Value;
        if (string.IsNullOrEmpty(newName))
        {
            return ApiResult<string>.Fail("Name must have a value");
        }

        return await _profileService.UpdateNameAsync(newName, UserId);
    }

    [HttpPost("change-password")]
    public async Task<ApiResult> ChangePassword(SingleValueDto<string> dto)
    {
        var newPassword = dto.Value;
        if (string.IsNullOrEmpty(newPassword))
        {
            return ApiResult.Fail("Password must have a value");
        }

        return await _profileService.ChangePasswordAsync(newPassword, UserId);
    }
}


