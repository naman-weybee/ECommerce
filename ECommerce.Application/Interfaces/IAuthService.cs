using ECommerce.Application.DTOs;

namespace ECommerce.Application.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(UserCreateDTO dto);

        Task<UserTokenDTO> LoginAsync(UserLoginDTO dto);

        Task RevokeRefreshTokenAsync(RevokeRefreshTokenDTO dto);

        Task<UserTokenDTO> ReCreateAccessTokenAsync(AccessTokenCreateDTO dto);

        Task<UserTokenDTO> GenerateUserTokenAsync(UserDTO dto);

        Task VerifyEmailAsync(string token);
    }
}