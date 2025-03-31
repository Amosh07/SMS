using SMS.Application.Common.Response;
using SMS.Application.Common.Service;
using SMS.Application.DTOs.Dashboard;

namespace SMS.Application.Interface.Services
{
    public interface IDashboardService : ITransientService
    {
        GetAdminCountDto GetAdminDashboardCount(int periodAction);
    }
}
