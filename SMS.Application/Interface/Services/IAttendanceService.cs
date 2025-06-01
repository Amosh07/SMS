using SMS.Application.Common.Service;
using SMS.Application.DTOs.Attendance;
using SMS.Application.DTOs.Class;

namespace SMS.Application.Interface.Services
{
    public interface IAttendanceService : ITransientService
    {
        void InsertAttendance(InsertAttendanceDto insertAttendanceDto);

        GetAttendanceDetailDto GetAttendanceDetails(Guid Id);

        Task<List<GetAttendanceDetailDto>> GetAllAttendanceDetails();

        void UpdateAttendanceDetails(UpdateAttendanceRequestDto attendance);

        void ActivateDeactivateAttendance(Guid attendanceId);

        List<GetAttendanceDetailDto> GetAllAttendanceDetails(int pageNumber, int pageSize, out int rowCount, string? search = null, bool? isActive = null);

    }
}
