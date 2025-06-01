using Microsoft.AspNetCore.Mvc;
using SMS.API.Controllers.Base;
using SMS.Application.Common.Response;
using SMS.Application.DTOs.Class;
using SMS.Application.DTOs.Exam;
using SMS.Application.Interface.Services;
using SMS.Identity.Implementation.Services;
using System.Net;

namespace SMS.API.Controllers
{
    [Route("api/exam")]
    public class ExamController(IExamService examService) : BaseController<ExamController>
    {
        [HttpPost("InsertExam")]
        public IActionResult InsertExam(InsertExamDto insertExamDto)
        {
            examService.InsertExam(insertExamDto);

            return Ok(new ResponseDto<bool>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Exam details insert successfully.",
                Result = true
            });
        }

        [HttpGet("{Id:guid}")]
        public IActionResult GetAttendanceDetails(Guid Id)
        {
            var result = examService.GetExamDetails(Id);

            return Ok(new ResponseDto<GetExamDetailDto>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Exam details successfully retrieved",
                Result = result
            });
        }

        [HttpPut]
        public IActionResult UpdateExamDetails(UpdateExamRequestDto exam)
        {
           examService.UpdateExamDetails(exam);

            return Ok(new ResponseDto<bool>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Exam details successfully updated.",
                Result = true
            });
        }
        [HttpGet("getAllExam")]
        public async Task<IActionResult> GetAllExamDetails()
        {
            var result = await examService.GetAllExamDetails();

            return Ok(new ResponseDto<List<GetExamDetailDto>>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Exam details fetched successfully",
                Result = result
            });
        }

        [HttpGet]
        public IActionResult GetAllExamDetails(int pageNumber, int pageSize, string? search, bool? isActive)
        {
            var result = examService.GetAllExamDetails(pageNumber, pageSize, out var rowCount, search, isActive);

            return Ok(new CollectionDto<GetExamDetailDto>(result, rowCount, pageNumber, pageSize)
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Exam retrival successfull",
                Result = result
            });

        }
    }
}