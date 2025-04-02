using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using SMS.API.Controllers.Base;
using SMS.Application.Common.Response;
using SMS.Application.DTOs.Identity;
using SMS.Application.Interface.Services.Identity;
using System.Net;

namespace SMS.API.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController(IAuthenticationService authenticationService) : BaseController<AuthenticationController>
    {

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginRequest)
        {
            var result = await authenticationService.Login(loginRequest);

            return Ok(new ResponseDto<UserLoginResponseDto>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Successfully authenticated.",
                Result = result,
            });
        }

        [HttpPost("user-registration")]
        public async Task<IActionResult> UserRegister([FromForm] UserRegisterDto registration)
        {
            var result = await authenticationService.UserRegister(registration);

            return Ok(new ResponseDto<RegistrationResponseDto>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = result.Message,
                Result = result,
            });
        }

        [AllowAnonymous]
        [HttpPost("Teacher-registration")]
        public async Task<IActionResult> TacherRegister([FromForm] TeacherRegisterDto registration)
        {
            var result = await authenticationService.TeacherRegister(registration);

            return Ok(new ResponseDto<RegistrationResponseDto>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = result.Message,
                Result = result
            });
        }

        [AllowAnonymous]
        [HttpPost("Student-registration")]
        public async Task<IActionResult> StudentRegister([FromForm] StudentRegisterDto registration)
        {
            var result = await authenticationService.StudentRegister(registration);

            return Ok(new ResponseDto<RegistrationResponseDto>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = result.Message,
                Result = result
            });
        }

        [HttpPost("logout")]
        public Task<IActionResult> Logout()
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            authenticationService.ExpireToken(accessToken);

            var result = new ResponseDto<bool>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "User successfully logged out.",
                Result = true
            };

            return Task.FromResult<IActionResult>(Ok(result));
        }

    }
}
