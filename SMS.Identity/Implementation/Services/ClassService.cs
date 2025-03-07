using SMS.Application.DTOs.Class;
using SMS.Application.Exceptions;
using SMS.Application.Interface.Repositories.Base;
using SMS.Application.Interface.Services;
using SMS.Domain.Entities;

namespace SMS.Identity.Implementation.Services
{
    public class ClassService(IGenericRepository genericRepository) : IClassService
    {
        public GetClassDetailDto GetClassDetails(Guid Id)
        {
            var classModel = genericRepository.GetById<Class>(Id)
                                            ?? throw new NotFoundException("The Class was not found.");

            var result = new GetClassDetailDto()
            {
                ClassName = classModel.ClassName,
                ClassNumber = classModel.ClassName
            };

            return result;
        }

        public void InsertClass(InsertClassDto insertClassDto)
        {
            throw new NotImplementedException();
        }
    }
}
