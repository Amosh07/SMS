using SMS.Application.Common.Service;
using SMS.Application.DTOs.Exam;

namespace SMS.Application.Interface.Services
{
    public interface IExamService : ITransientService
    {
        void InsertExam(InsertExamDto insertExamDto);

        GetExamDetailDto GetExamDetails(Guid Id);
    }
}
