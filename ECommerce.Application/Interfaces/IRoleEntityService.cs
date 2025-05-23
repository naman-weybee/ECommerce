using ECommerce.Application.DTOs;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IRoleEntityService
    {
        Task<List<RoleEntityDTO>> GetAllRoleEntitiesAsync(RequestParams requestParams);
    }
}