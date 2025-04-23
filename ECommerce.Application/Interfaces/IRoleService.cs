using ECommerce.Application.DTOs.Role;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleDTO>> GetAllRolesAsync(RequestParams? requestParams = null);

        Task<List<RoleDTO>> GetAllRolesByUserIdAsync(Guid userId);

        Task<RoleDTO> GetRoleByIdAsync(Guid id);

        Task<RoleDTO> GetSpecificRoleByUserAsync(Guid id, Guid userId);

        Task CreateRoleAsync(RoleCreateDTO dto);

        Task UpdateRoleAsync(RoleUpdateDTO dto);

        Task DeleteRoleAsync(Guid id);
    }
}