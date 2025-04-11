using SMS.Application.Common.Service;
using SMS.Application.DTOs.Class;

namespace SMS.Application.Interface.Services
{
    public interface IClassService : ITransientService
    {
        void InsertClass(InsertClassDto insertClassDto);

        GetClassDetailDto GetClassDetails(Guid Id);

        List<GetClassDetailDto> GetAllClassDetails();

        void UpdateClassDetails(UpdateClassRequestDto @class);

        void ActivateDeactivateClass(Guid classId);
    }
}
