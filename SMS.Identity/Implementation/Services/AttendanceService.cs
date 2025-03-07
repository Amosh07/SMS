using SMS.Application.DTOs.Attendance;
using SMS.Application.Exceptions;
using SMS.Application.Interface.Repositories.Base;
using SMS.Application.Interface.Services;
using SMS.Domain.Entities;

namespace SMS.Identity.Implementation.Services
{
    public class AttendanceService(IGenericRepository genericRepository) : IAttendanceService
    {
        public GetAttendanceDetailDto GetAttendanceDetails(Guid Id)
        {
            var attendanceModel = genericRepository.GetById<Attendance>(Id)
                                            ?? throw new NotFoundException("The Attendance was not found.");

            var result = new GetAttendanceDetailDto()
            {
                 Date = DateTime.Now,
            };

            return result;
        }

        public void InsertAttendance(InsertAttendanceDto insertAttendanceDto)
        {
            throw new NotImplementedException();
        }
    }
}