using ECommerce.Application.DTOs;
using ECommerce.Domain.Enums;

namespace ECommerce.API.Helper.Interfaces
{
    public interface IHTTPHelper
    {
        UserClaimsDTO GetClaims();

        Guid GetUserId();

        Task ValidateUserAuthorization(eRoleEntity roleEntity, eUserPermission userPermission);
    }
}