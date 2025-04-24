using ECommerce.Application.DTOs.User;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsersAsync(RequestParams? requestParams = null, bool useQuery = false);

        Task<List<UserDTO>> GetAllActiveUsersAsync(RequestParams? requestParams = null, bool useQuery = false);

        Task<UserDTO> GetUserByIdAsync(Guid id, bool useQuery = false);

        Task<UserDTO> GetUserByEmailAsync(string email, bool useQuery = false);

        Task UpsertUserAsync(UserUpsertDTO dto);

        Task PasswordResetAsync(PasswordResetDTO dto);

        Task VerifyEmailAsync(string token);

        Task DeleteUserAsync(Guid id);
    }
}