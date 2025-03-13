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
            var existingSubject = genericRepository.GetFirstOrDefault<Subject>(x => x.SubjectName == insertSubjectDto.SubjectName);

            if (existingSubject != null)
            {
                throw new NotFoundException("The following Subject is already exists");
            }
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
