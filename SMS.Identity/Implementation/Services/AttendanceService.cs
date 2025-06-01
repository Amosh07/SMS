using Microsoft.AspNetCore.Identity;
using SMS.Application.Common.User;
using SMS.Application.DTOs.Attendance;
using SMS.Application.Exceptions;
using SMS.Application.Interface.Repositories.Base;
using SMS.Application.Interface.Services;
using SMS.Domain.Entities;
using SMS.Domain.Entities.Identity;

namespace SMS.Identity.Implementation.Services
{
    public class AttendanceService(IGenericRepository genericRepository, ICurrentUserService currentUserService, UserManager<User> userManager) : IAttendanceService
    {
        public void ActivateDeactivateAttendance(Guid attendanceId)
        {
            var attendanceModel = genericRepository.GetById<Attendance>(attendanceId)
                             ?? throw new NotFoundException("The attendance was not found.");

            attendanceModel.IsActive = !attendanceModel.IsActive;

            genericRepository.Update(attendanceModel);
        }

        public async Task<List<GetAttendanceDetailDto>> GetAllAttendanceDetails()
        {
            var attendances = genericRepository.Get<Attendance>().ToList();
            var classes = genericRepository.Get<Class>().ToList();
            var users = genericRepository.Get<User>().ToList();

            var result = new List<GetAttendanceDetailDto>();

            foreach (var attendance in attendances)
            {
                var student = users.FirstOrDefault(u => u.Id == attendance.StudentId);
                var teacher = users.FirstOrDefault(u => u.Id == attendance.TeacherId);

                var dto = new GetAttendanceDetailDto
                {
                    Id = attendance.Id,
                    Date = attendance.Date,
                    ClassName = classes.FirstOrDefault(c => c.Id == attendance.ClassId)?.ClassName ?? "N/A",
                    IsActive = attendance.IsActive,
                    StudentName = student?.Name ?? "N/A",
                    TeacherName = teacher?.Name ?? "N/A"
                };

                result.Add(dto);
            }

            return result;
        }

        public List<GetAttendanceDetailDto> GetAllAttendanceDetails(int pageNumber, int pageSize, out int rowCount, string? search = null, bool? isActive = null)
        {
            // Step 1: Fetch paginated attendances with basic filtering
            var attendancePage = genericRepository.GetPagedResult<Attendance>(
                pageNumber,
                pageSize,
                out rowCount,
                x =>
                    (isActive == null || x.IsActive == isActive)
            ).ToList();

            // Step 2: Load related data once (avoid per-loop queries)
            var classes = genericRepository.Get<Class>().ToList();
            var users = genericRepository.Get<User>().ToList();

            // Step 3: Filter after loading if 'search' is provided
            if (!string.IsNullOrEmpty(search))
            {
                attendancePage = attendancePage.Where(a =>
                    (classes.FirstOrDefault(c => c.Id == a.ClassId)?.ClassName ?? "").ToLower().Contains(search.ToLower()) ||
                    (users.FirstOrDefault(u => u.Id == a.StudentId)?.Name ?? "").ToLower().Contains(search.ToLower()) ||
                    (users.FirstOrDefault(u => u.Id == a.TeacherId)?.Name ?? "").ToLower().Contains(search.ToLower())
                ).ToList();

                rowCount = attendancePage.Count;
            }

            // Step 4: Project to DTOs
            var result = attendancePage.Select(a =>
            {
                var student = users.FirstOrDefault(u => u.Id == a.StudentId);
                var teacher = users.FirstOrDefault(u => u.Id == a.TeacherId);
                var classObj = classes.FirstOrDefault(c => c.Id == a.ClassId);

                return new GetAttendanceDetailDto
                {
                    Id = a.Id,
                    Date = a.Date,
                    StudentName = student?.Name ?? "N/A",
                    TeacherName = teacher?.Name ?? "N/A",
                    ClassName = classObj?.ClassName ?? "N/A",
                    IsActive = a.IsActive
                };
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
                ClassId = insertAttendanceDto.ClassId,
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
            attendanceModel.StudentId = attendance.StudentId;
            attendanceModel.TeacherId = attendance.TeacherId;
            attendanceModel.ClassId = attendance.ClassId;

            genericRepository.Update(attendanceModel);
        }
    }
}