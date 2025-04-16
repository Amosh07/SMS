using SMS.Application.Common.User;
using SMS.Application.DTOs.Results;
using SMS.Application.Exceptions;
using SMS.Application.Interface.Repositories.Base;
using SMS.Application.Interface.Services;
using SMS.Domain.Entities;

namespace SMS.Identity.Implementation.Services
{
    public class ResultService(IGenericRepository genericRepository , ICurrentUserService currentUserService) : IResultService
    {
        public void ActivateDeactivateResult(Guid resultId)
        {
                var resultModel = genericRepository.GetById<Result>(resultId)
                 ?? throw new NotFoundException("The result was not found.");

                resultModel.IsActive = !resultModel.IsActive;

                genericRepository.Update(resultModel);
            }

        public List<GetResultDetailDto> GetAllResultDetails()
        {
            var results = genericRepository.Get<Result>();

            var result = results.Select(x => new GetResultDetailDto
            {
                Id = x.Id,
                Grade = x.Grade
            }).ToList();

            return result;
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
            //var existingResult = genericRepository.GetFirstOrDefault<Result>(x => x.Grade == insertResultDto.Grade);

            //if (existingResult != null)
            //{
            //    throw new NotFoundException("The following Results is already exists");
            //}
            var resultModel = new Result()
            {
                Grade = insertResultDto.Grade,
                StudentId = insertResultDto.StudentId, 
                ExamId = insertResultDto.ExamId,
                CreatedBy = currentUserService.GetUserId,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };
            genericRepository.Insert(resultModel);
        }

        public void UpdateResultDetails(UpdateResultRequestDto result)
        {
            var resultModel = genericRepository.GetById<Result>(result.Id)
                                    ?? throw new NotFoundException("The Result was not found.");

            resultModel.Grade = result.Grade;

            genericRepository.Update(resultModel);
        }
    }
}
