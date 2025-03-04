using SMS.Application.Common.Service;
using SMS.Application.DTOs.Identity;
using SMS.Application.DTOs.User;

namespace SMS.Application.Interface.Services.Identity
{
    public interface IUserService :  ITransientService
    {
        UserDetail GetUserProfileById(Guid userId);

        List<UserResponseDto> GetUsersByRole(string? search = null, Guid? roleId = default);

        void UpdateUserDetails(UpdateUserRequestDto user);

        string ResetUserPassword(ResetUserPasswordDto resetPassword);

        void ActivateDeactivateUsers(Guid userId);
    }
}
