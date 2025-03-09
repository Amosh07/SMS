using SMS.Application.DTOs.Exam;
using SMS.Application.Exceptions;
using SMS.Application.Interface.Repositories.Base;
using SMS.Application.Interface.Services;
using SMS.Domain.Entities;

namespace SMS.Identity.Implementation.Services
{
    public class ExamService(IGenericRepository genericRepository) : IExamService
    {
        public void ActivateDeactivateExam(Guid examId)
        {
            var examModel = genericRepository.GetById<Exam>(examId)
                             ?? throw new NotFoundException("The exam was not found.");

            examModel.IsActive = !examModel.IsActive;

            genericRepository.Update(examModel);
        }

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
            var existingExam = genericRepository.GetFirstOrDefault<Exam>(x => x.ExamName == insertExamDto.Examname);

            if (existingExam != null)
            {
                throw new NotFoundException("The following Exam is already exists");
            }
            var examModel = new Exam()
            {
                ExamName = insertExamDto.Examname,

                IsActive = true
            };
            genericRepository.Insert(examModel);
        }

        public void UpdateExamDetails(UpdateExamRequestDto exam)
        {
            var examModel = genericRepository.GetById<Exam>(exam.Id)
                                                ?? throw new NotFoundException("The Exam was not found.");
            examModel.ExamName = exam.Examname;

            genericRepository.Update(examModel);
        }
    }
}
