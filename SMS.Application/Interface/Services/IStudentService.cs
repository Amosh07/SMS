using SMS.Application.Common.Service;
using SMS.Application.DTOs.Student;
using SMS.Application.DTOs.Teacher;

namespace SMS.Application.Interface.Services
{
    public interface IStudentService : ITransientService
    {
        void InsertStudent(InsertStudentDto insertStudentDto);

        GetStudentDetailDto GetStudentDetails(Guid Id);
    }
}
