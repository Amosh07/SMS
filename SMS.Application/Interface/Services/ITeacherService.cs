using SMS.Application.Common.Service;
using SMS.Application.DTOs.Teacher;

namespace SMS.Application.Interface.Services
{
    public interface ITeacherService : ITransientService
   {
        GetTeacherDetailDto GetTeacherDetails(Guid Id);

        void UpdateTeacherDetails(UpdateTeacherRequestDto teacher);

        void ActivateDeactivateTeacher(Guid teacherId);
    }
}
