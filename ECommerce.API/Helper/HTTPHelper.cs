using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Enums;
using System.Security.Claims;

namespace ECommerce.API.Helper
{
    public class HTTPHelper : IHTTPHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRoleService _service;
        private readonly IMapper _mapper;

        public HTTPHelper(IHttpContextAccessor httpContextAccessor, IRoleService service, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _service = service;
            _mapper = mapper;
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
                throw new InvalidOperationException("User is Not Authenticated.");

            return Guid.Parse(httpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        public async Task ValidateUserAuthorization(eRoleEntity roleEntity, eUserPermission userPermission)
        {
            var roles = await GetUserRoles();
            if (roles.Count == 0)
                throw new UnauthorizedAccessException("Unauthorized User !!!");

            // Full Access Check
            if (roles.Any(role => role.RoleEntity == eRoleEntity.Full && role.HasFullPermission))
                return;

            // Specific Entity Access Check
            if (!roles.Any(role => role.RoleEntity == roleEntity))
                throw new UnauthorizedAccessException("Unauthorized User !!!");

            // Specific Entity and Full Permission Check
            if (roles.Any(role => role.RoleEntity == roleEntity && role.HasFullPermission))
                return;

            // Specific Entity and Specific Permission Check
            if (!roles.Any(role => role.RoleEntity == roleEntity && GetPropertyValue<bool>(role, userPermission)))
                throw new UnauthorizedAccessException("Unauthorized User !!!");
        }

        private async Task<List<RoleDTO>> GetUserRoles()
        {
            var roles = await _service.GetAllRolesByUserIdAsync(GetUserId());

            return _mapper.Map<List<RoleDTO>>(roles);
        }

        private static T GetPropertyValue<T>(object obj, Enum propertyName)
        {
            return obj.GetType().GetProperty(propertyName.ToString())?.GetValue(obj) is T value ? value : default!;
        }
    }
}