using ECommerce.Application.DTOs;
using ECommerce.Domain.Enums;

namespace ECommerce.API.Helper
{
    public interface IHTTPHelper
    {
        UserClaimsDTO GetClaims();

        Guid GetUserId();

        Task ValidateUserAuthorization(string entityName, eUserPermission userPermission);
    }
}