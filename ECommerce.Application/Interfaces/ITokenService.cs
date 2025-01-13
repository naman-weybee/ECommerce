using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(User user);
    }
}