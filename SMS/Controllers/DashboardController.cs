using Microsoft.AspNetCore.Mvc;
using SMS.API.Controllers.Base;
using SMS.Application.Common.Response;
using SMS.Application.DTOs.Dashboard;
using SMS.Application.Interface.Services;
using System.Net;

namespace SMS.API.Controllers
{
    [Route("api/dashboard")]
    public class DashboardController(IDashboardService dashboardService) : BaseController<DashboardController>
    {
        #region Admin
        [HttpGet("admin/dashboard-count/{period}")]
        public IActionResult GetAdminDashboardCount(int period)
        {
            var result = dashboardService.GetAdminDashboardCount(period);

            return Ok(new ResponseDto<GetAdminCountDto>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Admin dashboard count successfully fetched.",
                Result = result
            });
        }
        #endregion
    }
}
