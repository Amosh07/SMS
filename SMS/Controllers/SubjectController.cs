using Microsoft.AspNetCore.Mvc;
using SMS.API.Controllers.Base;
using SMS.Application.Common.Response;
using SMS.Application.DTOs.Student;
using SMS.Application.DTOs.Subject;
using SMS.Application.DTOs.Teacher;
using SMS.Application.Interface.Services;
using SMS.Identity.Implementation.Services;
using System.Net;

namespace SMS.API.Controllers
{
    [Route("api/subject")]
    public class SubjectController(ISubjectService subjectService) : BaseController<SubjectController>
    {
        [HttpPost("InsertStudent")]
        public IActionResult InsertStudent(InsertSubjectDto insertSubjectDto)
        {
            subjectService.InsertSubject(insertSubjectDto);

            return Ok(new ResponseDto<bool>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Subject details insert successfully.",
                Result = true
            });
        }

     [HttpGet("{Id:guid}")]
        public IActionResult GetSubjectDetails(Guid Id)
        {
            var result = subjectService.GetSubjectDetails(Id);

            return Ok(new ResponseDto<GetSubjectDetailDto>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Subject details successfully retrieved",
                Result = result
            });
        }
    }
}
