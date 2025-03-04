using Microsoft.AspNetCore.Mvc;
using SMS.API.Controllers.Base;
using SMS.Application.Common.Response;
using SMS.Application.DTOs.Identity;
using SMS.Application.DTOs.User;
using SMS.Application.Interface.Services.Identity;
using System.Net;

namespace SMS.API.Controllers
{
    [Route("api/user")]
    public class UserController(IUserService userService) : BaseController<UserController>
    {
        [HttpGet("{userId:guid}")]
        public IActionResult GetProfileByUserId(Guid userId)
        {
            var result = userService.GetUserProfileById(userId);

            return Ok(new ResponseDto<UserDetail>()
            {
                Message = "Profile successfully retrieved.",
                StatusCode = (int)HttpStatusCode.OK,
                Result = result
            });
        }

        [HttpGet("list")]
        public IActionResult GetUsersByRole(string? search, Guid? roleId)
        {
            var users = userService.GetUsersByRole(search, roleId);

            return Ok(new ResponseDto<List<UserResponseDto>>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Users successfully retrieved.",
                Result = users
            });
        }

        [HttpPut]
        public IActionResult UpdateUserDetails(UpdateUserRequestDto user)
        {
            userService.UpdateUserDetails(user);

            return Ok(new ResponseDto<bool>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "User details successfully updated.",
                Result = true
            });
        }

        [HttpPatch("{userId:guid}")]
        public IActionResult ActivateDeactivateUser(Guid userId)
        {
            userService.ActivateDeactivateUsers(userId);

            return Ok(new ResponseDto<bool>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "The status of user successfully updated.",
                Result = true
            });
        }
    }
}
