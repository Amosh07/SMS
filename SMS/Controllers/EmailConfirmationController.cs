using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMS.API.Controllers.Base;
using SMS.Application.Common.Response;
using SMS.Application.DTOs.EmailConfirmation;
using SMS.Application.Interface.Services;
using System.Net;

namespace SMS.API.Controllers
{
    [Route("api/email-confirmation")]
    public class EmailConfirmationController(IEmailConfirmationService emailConfirmationService) : BaseController<EmailConfirmationController>
    {
        [AllowAnonymous]
        [HttpPost("user-registration")]
        public async Task<IActionResult> UserRegistration(UserRegistrationResponseDto registrationEmail)
        {
            await emailConfirmationService.UserRegister(registrationEmail);

            return Ok(new ResponseDto<bool>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Confirmation email successfully sent, please check your inbox and follow the given instructions.",
                Result = true,
            });
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequestDto forgotPassword)
        {
            await emailConfirmationService.ForgotPassword(forgotPassword);

            return Ok(new ResponseDto<bool>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Email for forgot password successfully sent.",
                Result = true,
            });
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequestDto forgotPasswordEmail)
        {
            await emailConfirmationService.ResetPassword(forgotPasswordEmail);

            return Ok(new ResponseDto<bool>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Password for the following user has been successfully reset.",
                Result = true,
            });
        }

        [AllowAnonymous]
        [HttpPost("teacher-registration")]
        public async Task<IActionResult> TeacherRegistration(UserRegistrationResponseDto userRegistration)
        {
            await emailConfirmationService.TeacherRegister(userRegistration);

            return Ok(new ResponseDto<bool>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Confirmation email successfully sent, please check your inbox and follow the given instructions.",
                Result = true,
            });
        }

        [AllowAnonymous]
        [HttpPost("student-registration")]
        public async Task<IActionResult> StudentRegistration(UserRegistrationResponseDto userRegistration)
        {
            await emailConfirmationService.StudentRegister(userRegistration);

            return Ok(new ResponseDto<bool>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Confirmation email successfully sent, please check your inbox and follow the given instructions.",
                Result = true,
            });
        }
    }
    
}
