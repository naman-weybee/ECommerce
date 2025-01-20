using ECommerce.Application.DTOs;
using System.Security.Claims;

namespace ECommerce.API.Helper
{
    public static class HTTPHelper
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static UserClaimsDTO GetClaims()
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

        public static Guid GetUserId()
        {
            var httpContext = _httpContextAccessor?.HttpContext;

            if (httpContext == null || httpContext?.User?.Identity?.IsAuthenticated == false)
                throw new InvalidOperationException("User is not authenticated.");

            return Guid.Parse(httpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }
    }
}