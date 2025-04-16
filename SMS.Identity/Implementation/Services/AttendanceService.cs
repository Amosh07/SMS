using SMS.Application.Common.User;
using SMS.Application.DTOs.Attendance;
using SMS.Application.Exceptions;
using SMS.Application.Interface.Repositories.Base;
using SMS.Application.Interface.Services;
using SMS.Domain.Entities;

namespace SMS.Identity.Implementation.Services
{
    public class AttendanceService(IGenericRepository genericRepository, ICurrentUserService currentUserService) : IAttendanceService
    {
        public void ActivateDeactivateAttendance(Guid attendanceId)
        {
            var attendanceModel = genericRepository.GetById<Attendance>(attendanceId)
                             ?? throw new NotFoundException("The attendance was not found.");

            attendanceModel.IsActive = !attendanceModel.IsActive;

            genericRepository.Update(attendanceModel);
        }

        public List<GetAttendanceDetailDto> GetAllAttendanceDetails()
        {
            var attendances = genericRepository.Get<Attendance>();

            var result = attendances.Select(x => new GetAttendanceDetailDto
            {
                Id = x.Id,
                Date = x.Date
            }).ToList();

            return result;
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
            //var existingAttendance = genericRepository.GetFirstOrDefault<Attendance>(x => x.Date == insertAttendanceDto.Date);

            //if (existingAttendance != null)
            //{
            //    throw new NotFoundException("The following Attendance is already exists");
            //}
            var attendanceModel = new Attendance()
            {
                TeacherId = insertAttendanceDto.TeacherId,
                StudentId = insertAttendanceDto.StudentId,
                Date = insertAttendanceDto.Date,
                CreatedBy = currentUserService.GetUserId,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };
            genericRepository.Insert(attendanceModel);
        }

        public void UpdateAttendanceDetails(UpdateAttendanceRequestDto attendance)
        {
            var attendanceModel = genericRepository.GetById<Attendance>(attendance.Id)
                                                                        ?? throw new NotFoundException("The Attendance was not found.");
            attendanceModel.Date = attendance.Date;

            genericRepository.Update(attendanceModel);
        }
    }
}