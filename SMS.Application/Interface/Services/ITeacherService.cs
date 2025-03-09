using SMS.Application.Common.Service;
using SMS.Application.DTOs.Teacher;
using SMS.Application.DTOs.User;

namespace SMS.Application.Interface.Services
{
   public interface ITeacherService : ITransientService
   {
        void InsertTeacher(InsertTeacherDto insertTeacherDto);

        GetTeacherDetailDto GetTeacherDetails(Guid Id);

        void UpdateTeacherDetails(UpdateTeacherRequestDto teacher);

        void ActivateDeactivateTeacher(Guid teacherId);
    }
}
