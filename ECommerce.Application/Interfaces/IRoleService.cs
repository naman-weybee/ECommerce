using ECommerce.Application.DTOs.Role;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleDTO>> GetAllRolesAsync(RequestParams? requestParams = null, bool useQuery = false);

        Task<RoleDTO> GetRoleByUserIdAsync(Guid userId, bool useQuery = false);

        Task<RoleDTO> GetRoleByIdAsync(Guid id, bool useQuery = false);

        Task UpsertRoleAsync(RoleUpsertDTO dto);

        Task DeleteRoleAsync(Guid id);
    }
}