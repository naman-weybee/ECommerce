using ECommerce.Application.DTOs.Auth;
using ECommerce.Application.DTOs.RefreshToken;
using ECommerce.Application.DTOs.User;

namespace ECommerce.Application.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(UserUpsertDTO dto);

        Task<UserTokenDTO> LoginAsync(UserLoginDTO dto);

        Task RevokeRefreshTokenAsync(RevokeRefreshTokenDTO dto);

        Task<UserTokenDTO> ReCreateAccessTokenAsync(AccessTokenCreateDTO dto);

        Task<UserTokenDTO> GenerateUserTokenAsync(UserDTO dto);

        Task ReSendEmailVerificationAsync(ResendEmailVerificationDTO dto);

        Task VerifyEmailAsync(string token);
    }
}