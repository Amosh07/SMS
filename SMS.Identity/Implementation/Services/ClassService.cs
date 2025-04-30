using SMS.Application.Common.User;
using SMS.Application.DTOs.Class;
using SMS.Application.Exceptions;
using SMS.Application.Interface.Repositories.Base;
using SMS.Application.Interface.Services;
using SMS.Domain.Entities;

namespace SMS.Identity.Implementation.Services
{
    public class ClassService(IGenericRepository genericRepository, ICurrentUserService currentUserService) : IClassService
    {
        public void ActivateDeactivateClass(Guid classId)
        {
            var classModel = genericRepository.GetById<Class>(classId)
                 ?? throw new NotFoundException("The class was not found.");

            classModel.IsActive = !classModel.IsActive;

            genericRepository.Update(classModel);
        }

        public List<GetClassDetailDto> GetAllClassDetails()
        {
           var @class = genericRepository.Get<Class>();

            var result = @class.Select(x => new GetClassDetailDto
            {
                Id = x.Id,
                ClassName = x.ClassName,
                IsActive = x.IsActive
            }).ToList();

            return result;
        }

        public GetClassDetailDto GetClassDetails(Guid Id)
        {
            var classModel = genericRepository.GetById<Class>(Id)
                                            ?? throw new NotFoundException("The Class was not found.");

            var result = new GetClassDetailDto()
            {
                Id = classModel.Id,
                ClassName = classModel.ClassName,
            };

            return result;
        }

        public void InsertClass(InsertClassDto insertClassDto)
        {
            var existingClass = genericRepository.GetFirstOrDefault<Class>(x => x.ClassName == insertClassDto.ClassName);

            if (existingClass != null)
            {
                throw new NotFoundException("The following Class is already exists");
            }
            var classModel = new Class()
            {
                ClassName = insertClassDto.ClassName,
                CreatedBy = currentUserService.GetUserId,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };
            genericRepository.Insert(classModel);
        }

        public void UpdateClassDetails(UpdateClassRequestDto @class)
        {
            var classModel = genericRepository.GetById<Class>(@class.Id)
                                                            ?? throw new NotFoundException("The Class was not found.");
            classModel.ClassName = @class.ClassName;

            genericRepository.Update(classModel);
        }
    }
}
