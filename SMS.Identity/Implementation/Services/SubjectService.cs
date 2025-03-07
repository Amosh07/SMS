using SMS.Application.DTOs.Subject;
using SMS.Application.Exceptions;
using SMS.Application.Interface.Repositories.Base;
using SMS.Application.Interface.Services;
using SMS.Domain.Entities;

namespace SMS.Identity.Implementation.Services
{
    public class SubjectService(IGenericRepository genericRepository) : ISubjectService
    {
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
            throw new NotImplementedException();
        }
    }
}
