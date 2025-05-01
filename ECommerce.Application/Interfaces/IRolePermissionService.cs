using ECommerce.Application.DTOs.RolePermission;
using ECommerce.Domain.Enums;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IRolePermissionService
    {
        Task<List<RolePermissionDTO>> GetAllRolePermissionsAsync(RequestParams? requestParams = null, bool useQuery = false);

        Task<List<RolePermissionDTO>> GetAllRolePermissionsByRoleAsync(Guid roleId, RequestParams? requestParams = null, bool useQuery = false, bool isSortByPermission = false);

        Task<RolePermissionDTO> GetRolePermissionByIdsAsync(Guid roleId, eRoleEntity roleEntityId, bool useQuery = false);

        Task UpsertRolePermissionAsync(RolePermissionUpsertDTO dto);

        Task DeleteRolePermissionByRoleAsync(Guid roleId);

        Task DeleteRolePermissionAsync(Guid roleId, eRoleEntity roleEntityId);
    }
}