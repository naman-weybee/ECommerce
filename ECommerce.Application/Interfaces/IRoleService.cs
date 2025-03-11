using ECommerce.Application.DTOs;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleDTO>> GetAllRolesAsync(RequestParams requestParams);

        Task<List<RoleDTO>> GetAllRolesByUserIdAsync(Guid userId);

        Task<RoleDTO> GetRoleByIdAsync(Guid id);

        Task CreateRoleAsync(RoleCreateDTO dto);

        Task UpdateRoleAsync(RoleUpdateDTO dto);

        Task DeleteRoleAsync(Guid id);
    }
}