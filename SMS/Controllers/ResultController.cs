using Microsoft.AspNetCore.Mvc;
using SMS.API.Controllers.Base;
using SMS.Application.Common.Response;
using SMS.Application.DTOs.Class;
using SMS.Application.DTOs.Results;
using SMS.Application.Interface.Services;
using SMS.Identity.Implementation.Services;
using System.Net;

namespace SMS.API.Controllers
{
    [Route("api/result")]
    public class ResultController(IResultService resultService) : BaseController<ResultController>
    {
        [HttpPost("InsertResult")]
        public IActionResult InsertResult(InsertResultDto insertResultDto)
        {
            resultService.InsertResult(insertResultDto);

            return Ok(new ResponseDto<bool>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Result details insert successfully.",
                Result = true
            });
        }

        [HttpGet("{Id:guid}")]
        public IActionResult GetResultDetails(Guid Id)
        {
            var result = resultService.GetResultDetails(Id);

            return Ok(new ResponseDto<GetResultDetailDto>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Result details successfully retrieved",
                Result = result
            });
        }

        [HttpPut]
        public IActionResult UpdateResultDetails(UpdateResultRequestDto result)
        {
            resultService.UpdateResultDetails(result);

            return Ok(new ResponseDto<bool>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "User details successfully updated.",
                Result = true
            });
        }
        [HttpGet("getAllResult")]
        public IActionResult GetAllResultDetails()
        {
            var result = resultService.GetAllResultDetails();

            return Ok(new ResponseDto<List<GetResultDetailDto>>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Result details fetched successfully",
                Result = result
            });
        }
    }
}
