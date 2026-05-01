using Refit;
using TravelExpenseTracker.Shared.Dtos;

namespace TravelExpenseTracker.Apis;

[Headers("Authorization: Bearer ")]
public interface IProfileApi
{
    [Post("/api/me/update-name")]
    Task<ApiResult<string>> UpdateName(SingleValueDto<string> dto);

    [Post("/api/me/change-password")]
    Task<ApiResult> ChangePassword(SingleValueDto<string> dto);
}