using Hotel.Business.DTOs.RoleManageDTOs;

namespace Hotel.Business.Services.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleInfoDto>> GetAllRoles();
        Task<List<RoleInfoDto>> GetRoles(string userId);
        Task CreateRole(string roleName);
        Task DeleteRole(string roleName);
        Task UpdateRole(UpdateRoleDto updateRole);

    }
}
