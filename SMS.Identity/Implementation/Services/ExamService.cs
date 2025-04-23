using SMS.Application.Common.User;
using SMS.Application.DTOs.Exam;
using SMS.Application.Exceptions;
using SMS.Application.Interface.Repositories.Base;
using SMS.Application.Interface.Services;
using SMS.Domain.Entities;
using SMS.Domain.Entities.Identity;

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

        public async Task<List<GetExamDetailDto>> GetAllExamDetails()
        {
            var exams = genericRepository.Get<Exam>().ToList();
            var classes = genericRepository.Get<Class>().ToList();
            var subjects = genericRepository.Get<Subject>().ToList();
            var users = genericRepository.Get<User>().ToList();

            var result = new List<GetExamDetailDto>();

            foreach (var exam in exams)
            {
                var student = users.FirstOrDefault(u => u.Id == exam.StudentId);
                var subject = subjects.FirstOrDefault(s => s.Id == exam.SubjectId);
                var classEntity = classes.FirstOrDefault(c => c.Id == exam.ClassId);

                var dto = new GetExamDetailDto
                {
                    Id = exam.Id,
                    StudentName = student?.Name ?? "N/A",
                    SubjectName = subject?.SubjectName ?? "N/A",
                    Classes = classEntity?.ClassName ?? "N/A",
                    IsActive = exam.IsActive
                };

                result.Add(dto);
            }

            return result;
        }


        public GetExamDetailDto GetExamDetails(Guid Id)
        {
            var exam = genericRepository.GetById<Exam>(Id)
                                            ?? throw new NotFoundException("The Exam was not found.");

            var student = genericRepository.GetById<User>(exam.StudentId);
            var subject = genericRepository.GetById<Subject>(exam.SubjectId);
            var classEntity = genericRepository.GetById<Class>(exam.ClassId);

            var result = new GetExamDetailDto
            {
                Id = exam.Id,
                StudentName = student?.Name ?? "N/A",
                SubjectName = subject?.SubjectName ?? "N/A",
                Classes = classEntity?.ClassName ?? "N/A",
                IsActive = exam.IsActive
            };

            return result;
        }

        public void InsertExam(InsertExamDto insertExamDto)
        {
            var examModel = new Exam()
            {
                SubjectId = insertExamDto.SubjectId,
                ClassId = insertExamDto.ClassId,
                StudentId = insertExamDto.StudentId,
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
