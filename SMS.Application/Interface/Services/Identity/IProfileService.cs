using SMS.Application.Common.Service;
using SMS.Application.DTOs.Identity;

namespace SMS.Application.Interface.Services.Identity
{
    public interface IProfileService : ITransientService
    {
        UserDetail GetUserProfile();

        RolesDto GetUserRole();

        Task ChangePassword(ChangePasswordRequestDto changePasswordDto);

        void DeleteUserProfile();
    }
}
