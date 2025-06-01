using SMS.Application.Common.Service;
using SMS.Application.DTOs.Class;
using SMS.Application.DTOs.Exam;

namespace SMS.Application.Interface.Services
{
    public interface IExamService : ITransientService
    {
        void InsertExam(InsertExamDto insertExamDto);

        GetExamDetailDto GetExamDetails(Guid Id);

        Task<List<GetExamDetailDto>> GetAllExamDetails();

        void UpdateExamDetails(UpdateExamRequestDto exam);

        void ActivateDeactivateExam(Guid examId);

        List<GetExamDetailDto> GetAllExamDetails(int pageNumber, int pageSize, out int rowCount, string? search = null, bool? isActive = null);

    }
}
