using ECommerce.Application.DTOs.Auth;
using ECommerce.Application.DTOs.User;
using ECommerce.Domain.Entities;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsersAsync(RequestParams? requestParams = null, bool useQuery = false);

        Task<List<UserDTO>> GetAllActiveUsersAsync(RequestParams? requestParams = null, bool useQuery = false);

        Task<UserDTO> GetUserByIdAsync(Guid id, bool useQuery = false);

        Task<UserDTO> GetUserByEmailAsync(string email);

        Task UpsertUserAsync(UserUpsertDTO dto);

        Task SendVerificationEmailAsync(User entity);

        Task PasswordResetAsync(PasswordResetDTO dto);

        Task VerifyEmailAsync(string token);

        Task DeleteUserAsync(Guid id);
    }
}