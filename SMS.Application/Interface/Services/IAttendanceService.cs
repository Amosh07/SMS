using SMS.Application.Common.Service;
using SMS.Application.DTOs.Attendance;

namespace SMS.Application.Interface.Services
{
    public interface IAttendanceService : ITransientService
    {
        void InsertAttendance(InsertAttendanceDto insertAttendanceDto);

        GetAttendanceDetailDto GetAttendanceDetails(Guid Id);
    }
}
