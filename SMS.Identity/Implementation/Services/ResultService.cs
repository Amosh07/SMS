using SMS.Application.DTOs.Results;
using SMS.Application.Exceptions;
using SMS.Application.Interface.Repositories.Base;
using SMS.Application.Interface.Services;
using SMS.Domain.Entities;

namespace SMS.Identity.Implementation.Services
{
    public class ResultService(IGenericRepository genericRepository) : IResultService
    {
        public void ActivateDeactivateResult(Guid resultId)
        {
                var resultModel = genericRepository.GetById<Result>(resultId)
                 ?? throw new NotFoundException("The result was not found.");

                resultModel.IsActive = !resultModel.IsActive;

                genericRepository.Update(resultModel);
            }

        public GetResultDetailDto GetResultDetails(Guid Id)
        {
            var resultModel = genericRepository.GetById<Result>(Id)
                                            ?? throw new NotFoundException("The Result was not found.");

            var result = new GetResultDetailDto()
            {
                Grade = resultModel.Grade
            };

            return result;
        }

        public void InsertResult(InsertResultDto insertResultDto)
        {
            throw new NotImplementedException();
        }

        public void UpdateResultDetails(UpdateResultRequestDto result)
        {
            throw new NotImplementedException();
        }
    }
}
