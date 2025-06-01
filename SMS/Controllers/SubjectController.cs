using Microsoft.AspNetCore.Mvc;
using SMS.API.Controllers.Base;
using SMS.Application.Common.Response;
using SMS.Application.DTOs.Class;
using SMS.Application.DTOs.Subject;
using SMS.Application.Interface.Services;
using SMS.Identity.Implementation.Services;
using System.Net;

namespace SMS.API.Controllers
{
    [Route("api/subject")]
    public class SubjectController(ISubjectService subjectService) : BaseController<SubjectController>
    {
        [HttpPost("InsertSubject")]
        public IActionResult InsertSubject(InsertSubjectDto insertSubjectDto)
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
        [HttpPut]
        public IActionResult UpdateSubjectDetails(UpdateSubjectRequestDto subject)
        {
            subjectService.UpdateSubjectDetails(subject);

            return Ok(new ResponseDto<bool>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "User details successfully updated.",
                Result = true
            });
        }
        [HttpGet("getAllSubject")]
        public IActionResult GetAllSubjectDetails()
            {
                var result = subjectService.GetAllSubjectDetails();

                return Ok(new ResponseDto<List<GetSubjectDetailDto>>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Subject details fetched successfully",
                    Result = result
                });
        }

        [HttpGet]
        public IActionResult GetAllSubjectDetails(int pageNumber, int pageSize, string? search, bool? isActive)
        {
            var result = subjectService.GetAllSubjectDetails(pageNumber, pageSize, out var rowCount, search, isActive);

            return Ok(new CollectionDto<GetSubjectDetailDto>(result, rowCount, pageNumber, pageSize)
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Subject retrival successfull",
                Result = result
            });

        }
    }
}

