using SMS.Application.DTOs.Exam;
using SMS.Application.Exceptions;
using SMS.Application.Interface.Repositories.Base;
using SMS.Application.Interface.Services;
using SMS.Domain.Entities;

namespace SMS.Identity.Implementation.Services
{
    public class ExamService(IGenericRepository genericRepository) : IExamService
    {
        public GetExamDetailDto GetExamDetails(Guid Id)
        {
            var examModel = genericRepository.GetById<Exam>(Id)
                                            ?? throw new NotFoundException("The Exam was not found.");

            var result = new GetExamDetailDto()
            {
                ExamName = examModel.ExamName,
            };

            return result;
        }

        public void InsertExam(InsertExamDto insertExamDto)
        {
            throw new NotImplementedException();
        }
    }
}
