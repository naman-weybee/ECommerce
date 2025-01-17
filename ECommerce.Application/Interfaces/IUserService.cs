using ECommerce.Application.DTOs;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsersAsync(RequestParams requestParams);

        Task<UserDTO> GetUserByIdAsync(Guid id);

        Task CreateUserAsync(UserCreateDTO dto);

        Task UpdateUserAsync(UserUpdateDTO dto);

        Task VerifyEmailAsync(string token);

        Task DeleteUserAsync(Guid id);
    }
}