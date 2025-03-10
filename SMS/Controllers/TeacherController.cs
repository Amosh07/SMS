using Microsoft.AspNetCore.Mvc;
using SMS.API.Controllers.Base;
using SMS.Application.Common.Response;
using SMS.Application.DTOs.Teacher;
using SMS.Application.Interface.Services;
using System.Net;

namespace SMS.API.Controllers
{
    //[Route("api/teacher")]
    //public class TeacherController(ITeacherService teacherService) : BaseController<TeacherController>
    //{
    //    [HttpGet("{Id:guid}")]
    //    public IActionResult GetTeacherDetails(Guid Id)
    //    {
    //        var result = teacherService.GetTeacherDetails(Id);

    //        return Ok(new ResponseDto<GetTeacherDetailDto>()
    //        {
    //            StatusCode = (int)HttpStatusCode.OK,
    //            Message = "Teacher details successfully retrieved",
    //            Result = result
    //        });
    //    }

    //    [HttpPut]
    //    public IActionResult UpdateTeacherDetails(UpdateTeacherRequestDto teacher)
    //    {
    //        teacherService.UpdateTeacherDetails(teacher);

    //        return Ok(new ResponseDto<bool>()
    //        {
    //            StatusCode = (int)HttpStatusCode.OK,
    //            Message = "Teacher details successfully updated.",
    //            Result = true
    //        });
    //    }
    //}
}
