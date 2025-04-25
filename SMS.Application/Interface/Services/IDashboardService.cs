using SMS.Application.Common.Service;
using SMS.Application.DTOs.Dashboard;
using SMS.Application.DTOs.UserDashboard;

namespace SMS.Application.Interface.Services
{
    public interface IDashboardService : ITransientService
    {
        GetAdminCountDto GetAdminDashboardCount(int periodAction);
        GetUserCountDto GetUserDashboardCount(int periodAction);
    }
}
