using SMS.Application.Common.Service;
using SMS.Application.DTOs.Teacher;

namespace SMS.Application.Interface.Services
{
   public interface ITeacherService : ITransientService
   {
        void InsertTeacher(InsertTeacherDto insertTeacherDto);

        GetTeacherDetailDto GetTeacherDetails(Guid Id);
   }
}
