using SMS.Application.Common.User;
using SMS.Application.DTOs.Subject;
using SMS.Application.Exceptions;
using SMS.Application.Interface.Repositories.Base;
using SMS.Application.Interface.Services;
using SMS.Domain.Entities;

namespace SMS.Identity.Implementation.Services
{
    public class SubjectService(IGenericRepository genericRepository, ICurrentUserService currentUserService) : ISubjectService
    {
        public void ActivateDeactivateSubject(Guid subjectId)
        {
                var subjectModel = genericRepository.GetById<Subject>(subjectId)
                 ?? throw new NotFoundException("The subject was not found.");

                subjectModel.IsActive = !subjectModel.IsActive;

                genericRepository.Update(subjectModel);
            }

        public List<GetSubjectDetailDto> GetAllSubjectDetails()
        {
            var subjects = genericRepository.Get<Subject>().ToList();

            var classes = genericRepository.Get<Class>().ToList();

            var result = subjects.Select(subject => new GetSubjectDetailDto
            {
                Id = subject.Id,
                SubjectName = subject.SubjectName,
                ClassName = classes.FirstOrDefault(c => c.Id == subject.ClassId)!.ClassName ?? "N/A",
                IsActive = subject.IsActive,
            }).OrderBy(x => x.ClassName)
            .ThenBy(x => x.SubjectName)
            .ToList();

            return result;
        }

        public GetSubjectDetailDto GetSubjectDetails(Guid Id)
        {
            var SubjectModel = genericRepository.GetById<Subject>(Id)
                                ?? throw new NotFoundException("The Subject was not found.");

            var result = new GetSubjectDetailDto()
            {
                SubjectName = SubjectModel.SubjectName
               
            };

            return result;
        }

        public void InsertSubject(InsertSubjectDto insertSubjectDto)
        {
            var subjectModel = new Subject()
            {
                SubjectName = insertSubjectDto.SubjectName,
                ClassId = insertSubjectDto.ClassId,
                CreatedBy = currentUserService.GetUserId,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };
            genericRepository.Insert(subjectModel);
        }

        public void UpdateSubjectDetails(UpdateSubjectRequestDto subject)
        {
            var subjectModel = genericRepository.GetById<Subject>(subject.Id)
                        ?? throw new NotFoundException("The Subject was not found.");

            subjectModel.SubjectName = subject.SubjectName;
            
            genericRepository.Update(subjectModel);
        }
    }
}
