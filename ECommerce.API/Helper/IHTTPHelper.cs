using ECommerce.Application.DTOs;

namespace ECommerce.API.Helper
{
    public interface IHTTPHelper
    {
        UserClaimsDTO GetClaims();

        Guid GetUserId();
    }
}