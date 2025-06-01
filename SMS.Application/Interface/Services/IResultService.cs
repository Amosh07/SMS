using SMS.Application.Common.Service;
using SMS.Application.DTOs.Attendance;
using SMS.Application.DTOs.Results;
using SMS.Application.DTOs.Subject;

namespace SMS.Application.Interface.Services
{
    public interface IResultService : ITransientService
    {
        void InsertResult(InsertResultDto insertResultDto);

        GetResultDetailDto GetResultDetails(Guid Id);

        List<GetResultDetailDto> GetAllResultDetails();

        void UpdateResultDetails(UpdateResultRequestDto result);

        void ActivateDeactivateResult(Guid resultId);

        List<GetResultDetailDto> GetAllResultDetails(int pageNumber, int pageSize, out int rowCount, string? search = null, bool? isActive = null);

    }
}
