using Microsoft.AspNetCore.Mvc;
using SMS.API.Controllers.Base;
using SMS.Application.Common.Response;
using SMS.Application.DTOs.Results;
using SMS.Application.Interface.Services;
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
    }
}
