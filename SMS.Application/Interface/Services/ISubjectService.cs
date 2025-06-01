using SMS.Application.Common.Service;
using SMS.Application.DTOs.Results;
using SMS.Application.DTOs.Subject;

namespace SMS.Application.Interface.Services
{
    public interface ISubjectService : ITransientService
    {
        void InsertSubject(InsertSubjectDto insertSubjectDto);

        GetSubjectDetailDto GetSubjectDetails(Guid Id);

        List<GetSubjectDetailDto> GetAllSubjectDetails();

        void UpdateSubjectDetails(UpdateSubjectRequestDto subject);

        void ActivateDeactivateSubject(Guid subjectId);

        List<GetSubjectDetailDto> GetAllSubjectDetails(int pageNumber, int pageSize, out int rowCount, string? search = null, bool? isActive = null);

    }
}
