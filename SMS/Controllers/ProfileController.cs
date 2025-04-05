using Microsoft.AspNetCore.Mvc;
using SMS.API.Controllers.Base;
using SMS.Application.Common.Response;
using SMS.Application.DTOs.Identity;
using SMS.Application.Interface.Services.Identity;
using System.Net;

namespace SMS.API.Controllers
{
    [Route("api/profile")]
    public class ProfileController(IProfileService profileService) : BaseController<ProfileController>
    {
        [HttpGet]
        public IActionResult GetUserProfile()
        {
            var result = profileService.GetUserProfile();

            return Ok(new ResponseDto<UserDetail>()
            {
                Message = "Profile successfully retrieved.",
                StatusCode = (int)HttpStatusCode.OK,
                Result = result
            });
        }

        [HttpGet("role")]
        public IActionResult GetUserRole()
        {
            var result = profileService.GetUserRole();

            return Ok(new ResponseDto<RolesDto>()
            {
                Message = "Role successfully retrieved.",
                StatusCode = (int)HttpStatusCode.OK,
                Result = result
            });
        }

        [HttpPut("password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequestDto changePassword)
        {
            await profileService.ChangePassword(changePassword);

            return Ok(new ResponseDto<bool>()
            {
                Message = "Password changed successfully.",
                StatusCode = (int)HttpStatusCode.OK,
                Result = true
            });
        }

        [HttpDelete]
        public IActionResult DeleteUserProfile()
        {
            profileService.DeleteUserProfile();

            return Ok(new ResponseDto<bool>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "User successfully deleted.",
                Result = true
            });
        }
    }
}
