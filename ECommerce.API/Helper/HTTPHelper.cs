using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Enums;
using System.Security.Claims;

namespace ECommerce.API.Helper
{
    public class HTTPHelper : IHTTPHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _service;

        public HTTPHelper(IHttpContextAccessor httpContextAccessor, IUserService service)
        {
            _httpContextAccessor = httpContextAccessor;
            _service = service;
        }

        public UserClaimsDTO GetClaims()
        {
            var httpContext = _httpContextAccessor?.HttpContext;

            if (httpContext == null || httpContext?.User?.Identity?.IsAuthenticated == false)
                throw new InvalidOperationException("User is not authenticated.");

            return new UserClaimsDTO()
            {
                UserId = httpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!,
                Email = httpContext!.User.FindFirstValue(ClaimTypes.Email)!,
                Role = httpContext!.User.FindFirstValue(ClaimTypes.Role)!
            };
        }

        public Guid GetUserId()
        {
            var httpContext = _httpContextAccessor?.HttpContext;

            if (httpContext == null || httpContext?.User?.Identity?.IsAuthenticated == false)
                throw new InvalidOperationException("User is not authenticated.");

            return Guid.Parse(httpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        public async Task ValidateUserAuthorization(string entityName, eUserPermission userPermission)
        {
            var role = await GetUserRole();

            if (GetPropertyValue<bool>(role, eUserPermission.HasFullPermission))
                return;

            if (!role.EntityName.Equals(entityName, StringComparison.OrdinalIgnoreCase))
                throw new UnauthorizedAccessException("Unauthorized User !!!");

            if (!GetPropertyValue<bool>(role, userPermission))
                throw new UnauthorizedAccessException("Unauthorized User !!!");
        }

        private async Task<RoleDTO> GetUserRole()
        {
            var user = await _service.GetUserByIdAsync(GetUserId());

            return new RoleDTO
            {
                EntityName = user.Role.EntityName,
                HasViewPermission = user.Role.HasViewPermission,
                HasCreateOrUpdatePermission = user.Role.HasCreateOrUpdatePermission,
                HasDeletePermission = user.Role.HasDeletePermission,
            };
        }

        private static T GetPropertyValue<T>(object obj, Enum propertyName)
        {
            return obj.GetType().GetProperty(propertyName.ToString())?.GetValue(obj) is T value ? value : default!;
        }
    }
}