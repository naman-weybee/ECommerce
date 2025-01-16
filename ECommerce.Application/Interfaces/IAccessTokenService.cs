using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces
{
    public interface IAccessTokenService
    {
        Task<string> CreateAccessTokenAsync(User user);
    }
}