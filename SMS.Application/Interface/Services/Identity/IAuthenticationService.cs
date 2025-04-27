using SMS.Application.Common.Service;
using SMS.Application.DTOs.EmailConfirmation;
using SMS.Application.DTOs.Identity;

namespace SMS.Application.Interface.Services.Identity
{
  public  interface IAuthenticationService : ITransientService
   {
        Task<UserLoginResponseDto> Login(LoginDto login);

        Task<RegistrationResponseDto> UserRegister(UserRegisterDto user);

        Task<RegistrationResponseDto> TeacherRegister(TeacherRegisterDto candidate);

        Task<RegistrationResponseDto> StudentRegister(StudentRegisterDto candidate);

        void ExpireToken(string token);

        bool IsTokenExpired(string token);
    }
}
