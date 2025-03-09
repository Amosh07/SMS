using SMS.Application.Common.Service;
using SMS.Application.DTOs.Subject;

namespace SMS.Application.Interface.Services
{
    public interface ISubjectService : ITransientService
    {
        void InsertSubject(InsertSubjectDto insertSubjectDto);

        GetSubjectDetailDto GetSubjectDetails(Guid Id);

        void UpdateSubjectDetails(UpdateSubjectRequestDto subject);

        void ActivateDeactivateSubject(Guid subjectId);
    }
}
