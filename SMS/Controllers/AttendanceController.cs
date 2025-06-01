using Microsoft.AspNetCore.Mvc;
using SMS.API.Controllers.Base;
using SMS.Application.Common.Response;
using SMS.Application.DTOs.Attendance;
using SMS.Application.Interface.Services;
using System.Net;

namespace SMS.API.Controllers
{
    [Route("api/attendance")]
    public class AttendanceController(IAttendanceService attendanceService) : BaseController<AttendanceController>
    {
        [HttpPost("InsertAttendance")]
        public IActionResult InsertAttendance(InsertAttendanceDto insertAttendanceDto)
        {
            attendanceService.InsertAttendance(insertAttendanceDto);

            return Ok(new ResponseDto<bool>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Attendance details insert successfully.",
                Result = true
            });
        }

        [HttpGet("{Id:guid}")]
        public IActionResult GetAttendanceDetails(Guid Id)
        {
            var result = attendanceService.GetAttendanceDetails(Id);

            return Ok(new ResponseDto<GetAttendanceDetailDto>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Attendance details successfully retrieved",
                Result = result
            });
        }

        [HttpPut]
        public IActionResult UpdateAttendanceDetails(UpdateAttendanceRequestDto attendance)
        {
            attendanceService.UpdateAttendanceDetails(attendance);

            return Ok(new ResponseDto<bool>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Attendance details successfully updated.",
                Result = true
            });
        }

        [HttpGet("getAllAttendance")]
        public async Task<IActionResult> GetAllAttendanceDetails()
        {
            var result = await attendanceService.GetAllAttendanceDetails();

            return Ok(new ResponseDto<List<GetAttendanceDetailDto>>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Attendance details fetched successfully",
                Result = result
            });
        }

        [HttpGet]
        public IActionResult GetAllAttendanceDetails(int pageNumber, int pageSize, string? search, bool? isActive)
        {
            var result = attendanceService.GetAllAttendanceDetails(pageNumber, pageSize, out var rowCount, search, isActive);

            return Ok(new CollectionDto<GetAttendanceDetailDto>(result, rowCount, pageNumber, pageSize)
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Attendance retrival successfull",
                Result = result
            });

        }
    }
}
