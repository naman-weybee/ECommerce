using ECommerce.Application.DTOs;
using System.Security.Claims;

namespace ECommerce.API.Helper
{
    public class HTTPHelper : IHTTPHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HTTPHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
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
    }
}