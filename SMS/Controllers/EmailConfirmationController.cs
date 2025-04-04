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
    }
}
