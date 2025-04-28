using AutoMapper;
using ECommerce.API.Helper.Interfaces;
using ECommerce.Application.DTOs.RolePermission;
using ECommerce.Application.DTOs.User;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Enums;
using System.Security.Claims;

namespace ECommerce.API.Helper
{
    public class HTTPHelper : IHTTPHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRoleService _roleService;
        private readonly IRolePermissionService _rolePermissionService;
        private readonly IMapper _mapper;

        public HTTPHelper(IHttpContextAccessor httpContextAccessor, IRoleService service, IRolePermissionService rolePermissionService, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _roleService = service;
            _rolePermissionService = rolePermissionService;
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
            var roles = await GetUserRolePermissons();
            if (roles.Count == 0)
                throw new UnauthorizedAccessException("Unauthorized User !!!");

            // Full Access Check
            if (roles.Any(role => role.RoleEntityId == eRoleEntity.Full && role.HasFullPermission))
                return;

            // Specific Entity Access Check
            if (!roles.Any(role => role.RoleEntityId == roleEntity))
                throw new UnauthorizedAccessException("Unauthorized User !!!");

            // Specific Entity and Full Permission Check
            if (roles.Any(role => role.RoleEntityId == roleEntity && role.HasFullPermission))
                return;

            // Specific Entity and Specific Permission Check
            if (!roles.Any(role => role.RoleEntityId == roleEntity && GetPropertyValue<bool>(role, userPermission)))
                throw new UnauthorizedAccessException("Unauthorized User !!!");
        }

        private async Task<List<RolePermissionDTO>> GetUserRolePermissons()
        {
            var userRole = await _roleService.GetRoleByUserIdAsync(GetUserId());
            var rolePermissions = await _rolePermissionService.GetAllRolePermissionsByRoleAsync(userRole.Id, isSortByPermission: true);

            return _mapper.Map<List<RolePermissionDTO>>(rolePermissions);
        }

        private static T GetPropertyValue<T>(object obj, Enum propertyName)
        {
            return obj.GetType().GetProperty(propertyName.ToString())?.GetValue(obj) is T value ? value : default!;
        }
    }
}