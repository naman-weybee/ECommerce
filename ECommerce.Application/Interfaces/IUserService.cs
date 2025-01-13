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

        Task DeleteUserAsync(Guid id);

        Task<UserDTO> GetUserByEmailAndPasswordAsync(string email, string password);
    }
}