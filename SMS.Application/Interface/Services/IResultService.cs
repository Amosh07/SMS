using SMS.Application.Common.Service;
using SMS.Application.DTOs.Results;

namespace SMS.Application.Interface.Services
{
    public interface IResultService : ITransientService
    {
        void InsertResult(InsertResultDto insertResultDto);

        GetResultDetailDto GetResultDetails(Guid Id);
    }
}
