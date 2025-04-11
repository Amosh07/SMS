using Microsoft.AspNetCore.Mvc;
using SMS.API.Controllers.Base;
using SMS.Application.Common.Response;
using SMS.Application.DTOs.Class;
using SMS.Application.Interface.Services;
using System.Net;

namespace SMS.API.Controllers
{
    [Route("api/class")]
    public class ClassController(IClassService classService) : BaseController<ClassController>
    {
        [HttpPost("InsertClass")]
        public IActionResult InsertClass(InsertClassDto insertClassDto)
        {
           classService.InsertClass(insertClassDto);

            return Ok(new ResponseDto<bool>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Class details insert successfully.",
                Result = true
            });
        }

        [HttpGet("{Id:guid}")]
        public IActionResult GetClassDetails(Guid Id)
        {
            var result = classService.GetClassDetails(Id);

            return Ok(new ResponseDto<GetClassDetailDto>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Class details successfully retrieved",
                Result = result
            });
        }

        [HttpPut]
        public IActionResult UpdateClassDetails(UpdateClassRequestDto @class)
        {
            classService.UpdateClassDetails(@class);

            return Ok(new ResponseDto<bool>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Exam details successfully updated.",
                Result = true
            });
        }

        [HttpGet("getAllClass")]
        public IActionResult GetAllClassDetails()
        {
            var result = classService.GetAllClassDetails();

            return Ok(new ResponseDto<List<GetClassDetailDto>>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Class details fetched successfully",
                Result = result
            });
        }
    }
}
