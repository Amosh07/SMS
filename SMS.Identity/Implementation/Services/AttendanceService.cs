using SMS.Application.DTOs.Attendance;
using SMS.Application.Exceptions;
using SMS.Application.Interface.Repositories.Base;
using SMS.Application.Interface.Services;
using SMS.Domain.Entities;

namespace SMS.Identity.Implementation.Services
{
    public class AttendanceService(IGenericRepository genericRepository) : IAttendanceService
    {
        public void ActivateDeactivateAttendance(Guid attendanceId)
        {
            var attendanceModel = genericRepository.GetById<Attendance>(attendanceId)
                             ?? throw new NotFoundException("The attendance was not found.");

            attendanceModel.IsActive = !attendanceModel.IsActive;

            genericRepository.Update(attendanceModel);
        }

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

        public void UpdateAttendanceDetails(UpdateAttendanceRequestDto attendance)
        {
            throw new NotImplementedException();
        }
    }
}