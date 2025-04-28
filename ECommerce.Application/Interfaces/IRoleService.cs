using ECommerce.Application.DTOs.Role;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleDTO>> GetAllRolesAsync(RequestParams? requestParams = null);

        Task<RoleDTO> GetRoleByUserIdAsync(Guid userId);

        Task<RoleDTO> GetRoleByIdAsync(Guid id);

        Task<RoleDTO> GetSpecificRoleByUserAsync(Guid id, Guid userId);

        Task UpsertRoleAsync(RoleUpsertDTO dto);

        Task DeleteRoleAsync(Guid id);
    }
}