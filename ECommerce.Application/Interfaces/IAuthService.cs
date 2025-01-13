using ECommerce.Application.DTOs;

namespace ECommerce.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(UserLoginDTO dto);

        Task RegisterAsync(UserCreateDTO dto);
    }
}