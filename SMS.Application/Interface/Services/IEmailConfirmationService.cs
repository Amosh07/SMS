using SMS.Application.Common.Service;
using SMS.Application.DTOs.EmailConfirmation;

namespace SMS.Application.Interface.Services
{
    public interface IEmailConfirmationService : ITransientService
    {
        Task UserRegister(UserRegistrationResponseDto registerEmail);

        Task ForgotPassword(ForgotPasswordRequestDto forgotPasswordDto);

        Task ResetPassword(ResetPasswordRequestDto forgotPasswordDto);

        Task RequestAction(RequestActionResponseDto request);

        Task ResetUserPassword(ResetUserPasswordRequestDto resetPasswordRequest);

        Task TeacherRegister(UserRegistrationResponseDto registrationEmail);

        Task StudentRegister(UserRegistrationResponseDto registrationEmail);
    }
}
