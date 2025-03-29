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
        [HttpGet("user/{userId:guid}")]
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

        [HttpGet("teacher/{teacherId:guid}")]
        public IActionResult GetProfileByTeacherId(Guid teacherId)
        {
            var result = userService.GetAllTeacherById(teacherId);

            return Ok(new ResponseDto<TeacherDetails>()
            {
                Message = " Teacher profile successfully retrieved.",
                StatusCode = (int)HttpStatusCode.OK,
                Result = result
            });
        }

        [HttpGet("student/{studentId:guid}")]
        public IActionResult GetProfileByStudentId(Guid studentId)
        {
            var result = userService.GetAllStudentById(studentId);

            return Ok(new ResponseDto<StudentDetails>()
            {
                Message = " Teacher profile successfully retrieved.",
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

        [HttpGet("Teacher-List")]
        public IActionResult GetTeacherList()
        {
            var result = userService.GetTeacherList();

            return Ok(new ResponseDto<List<TeacherDetails>>()
            {
                Message = "Profile successfully retrieved.",
                StatusCode = (int)HttpStatusCode.OK,
                Result = result
            });
        }

        [HttpGet("Student-List")]
        public IActionResult GetStudentList()
        {
            var result = userService.GetStudentList();

            return Ok(new ResponseDto<List<StudentDetails>>()
            {
                Message = "Profile successfully retrieved.",
                StatusCode = (int)HttpStatusCode.OK,
                Result = result
            });
        }
    }
}
