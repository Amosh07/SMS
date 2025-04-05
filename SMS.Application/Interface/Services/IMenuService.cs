using SMS.Application.Common.Service;
using SMS.Application.DTOs.Menu;

namespace SMS.Application.Interface.Services
{
    public interface IMenuService : ITransientService
    {
        void AssignMenus(RoleMenuRequestDto roleMenu);

        List<RoleMenuResponseDto> GetAllRoleMenus(Guid roleId);

        List<RoleMenuResponseDto> GetAllAssignedMenus();
    }
}
