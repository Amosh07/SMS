using Microsoft.AspNetCore.Mvc;
using SMS.API.Controllers.Base;
using SMS.Application.Common.Response;
using SMS.Application.DTOs.Student;
using SMS.Application.Interface.Services;
using System.Net;

namespace SMS.API.Controllers
{
    [Route("api/student")]
    public class StudentController(IStudentService studentService) : BaseController<StudentController>
    {
        [HttpPost("InsertStudent")]
        public IActionResult InsertStudent(InsertStudentDto insertStudentDto)
        {
            studentService.InsertStudent(insertStudentDto);

            return Ok(new ResponseDto<bool>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Student details insert successfully.",
                Result = true
            });
        }

        [HttpGet("{Id:guid}")]
        public IActionResult GetTeacherDetails(Guid Id)
        {
            var result = studentService.GetStudentDetails(Id);

            return Ok(new ResponseDto<GetStudentDetailDto>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Student details successfully retrieved",
                Result = result
            });
        }
        [HttpPut]
        public IActionResult UpdateStudentDetails(UpdateStudentRequestDto student)
        {
            studentService.UpdateStudentDetails(student);

            return Ok(new ResponseDto<bool>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Student details successfully updated.",
                Result = true
            });
        }
    }
}
