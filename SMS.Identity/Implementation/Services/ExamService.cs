using SMS.Application.Common.User;
using SMS.Application.DTOs.Exam;
using SMS.Application.Exceptions;
using SMS.Application.Interface.Repositories.Base;
using SMS.Application.Interface.Services;
using SMS.Domain.Entities;

namespace SMS.Identity.Implementation.Services
{
    public class ExamService(IGenericRepository genericRepository , ICurrentUserService currentUserService) : IExamService
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
               // SubjectId = examModel.SubjectId.ToString()
            };

            return result;
        }

        public void InsertExam(InsertExamDto insertExamDto)
        {
            /*var existingExam = genericRepository.GetFirstOrDefault<Exam>(x => x.Id == insertExamDto.SubjectId);

             if (existingExam != null)
             {
                 throw new NotFoundException("The following Exam is already exists");
             }*/

            var examModel = new Exam()
            {
                SubjectId = insertExamDto.SubjectId,
                ClassId = insertExamDto.ClassId,
                CreatedBy = currentUserService.GetUserId,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };
            genericRepository.Insert(examModel);
        }

        public void UpdateExamDetails(UpdateExamRequestDto exam)
        {
            var examModel = genericRepository.GetById<Exam>(exam.Id)
                                                ?? throw new NotFoundException("The Exam was not found.");
            examModel.SubjectId = exam.SubjectId;

            genericRepository.Update(examModel);
        }
    }
}
