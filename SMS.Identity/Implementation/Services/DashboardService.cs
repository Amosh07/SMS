using SMS.Application.Common.User;
using SMS.Application.DTOs.Dashboard;
using SMS.Application.DTOs.UserDashboard;
using SMS.Application.Exceptions;
using SMS.Application.Interface.Repositories.Base;
using SMS.Application.Interface.Services;
using SMS.Domain.Common;
using SMS.Domain.Entities.Identity;

namespace SMS.Identity.Implementation.Services
{
    public class DashboardService(IGenericRepository genericRepository, ICurrentUserService currentUserService) : IDashboardService
    {
        public GetAdminCountDto GetAdminDashboardCount(int periodAction)
        {
            var userRole = genericRepository.GetFirstOrDefault<Role>(r => r.Name == Constants.Roles.User);

            var userRoles = genericRepository.Get<UserRoles>(ur => ur.RoleId == userRole.Id).ToList();

            var teacherRole = genericRepository.GetFirstOrDefault<Role>(r => r.Name== Constants.Roles.Teacher)
                 ?? throw new NotFoundException("The following teacher role has not been registered to the system.");

            var teacherUserRoles = genericRepository.Get<UserRoles>(ur => ur.RoleId == teacherRole.Id).ToList();

            var studentRole = genericRepository.GetFirstOrDefault<Role>(r=> r.Name == Constants.Roles.Student);

            var studentUserRoles = genericRepository.Get<UserRoles>(ur => ur.RoleId == studentRole.Id).ToList();

            return new GetAdminCountDto
            {
                TotalStudent = studentUserRoles.Count,
                TotalTeacher = teacherUserRoles.Count,
                TotalUser = userRoles.Count
            };
        }

        public GetUserCountDto GetUserDashboardCount(int periodAction)
        {
            var teacherRole = genericRepository.GetFirstOrDefault<Role>(r => r.Name == Constants.Roles.Teacher)
                ?? throw new NotFoundException("The following teacher role has not been registered to the system.");

            var teacherUserRoles = genericRepository.Get<UserRoles>(ur => ur.RoleId == teacherRole.Id).ToList();

            var studentRole = genericRepository.GetFirstOrDefault<Role>(r => r.Name == Constants.Roles.Student)
                ?? throw new NotFoundException("The following student role has not been registered to the system.");

            var studentUserRoles = genericRepository.Get<UserRoles>(ur => ur.RoleId == studentRole.Id).ToList();

            return new GetUserCountDto
            {
                TotalTeachersAdded = teacherUserRoles.Count,
                TeachersGrowthPercent = 0,
                TotalStudentsAdded = studentUserRoles.Count,
                StudentsGrowthPercent = 0, 
                RecentActivityCount = 0, 
                LastUpdated = DateTime.Now
            };
        }
    }
}
