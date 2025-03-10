using System.Net;

namespace ECommerce.API.Helper
{
    public static class ExceptionHelper
    {
        private static readonly Dictionary<Type, HttpStatusCode> ExceptionStatusCodeMap = new()
        {
            { typeof(UnauthorizedAccessException), HttpStatusCode.Unauthorized },
            { typeof(ArgumentException), HttpStatusCode.BadRequest },
            { typeof(ArgumentNullException), HttpStatusCode.BadRequest },
            { typeof(KeyNotFoundException), HttpStatusCode.NotFound },
            { typeof(InvalidOperationException), HttpStatusCode.Conflict },
            { typeof(Exception), HttpStatusCode.InternalServerError }
        };

        public static HttpStatusCode GetStatusCode(Exception ex)
        {
            return ExceptionStatusCodeMap.TryGetValue(ex.GetType(), out var statusCode)
                ? statusCode
                : HttpStatusCode.InternalServerError;
        }
    }
}