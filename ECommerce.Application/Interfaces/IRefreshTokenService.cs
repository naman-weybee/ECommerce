using ECommerce.Application.DTOs;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<List<RefreshTokenDTO>> GetAllRefreshTokensAsync(RequestParams requestParams);

        Task<RefreshTokenDTO> GetRefreshTokenByIdAsync(Guid id);

        Task<RefreshTokenDTO> CreateRefreshTokenAsync(RefreshTokenCreateDTO dto);

        Task UpdateRefreshTokenAsync(RefreshTokenUpdateDTO dto);

        Task DeleteRefreshTokenAsync(Guid id);

        Task RevokeRefreshTokenAsync(RevokeRefreshTokenDTO dto);
    }
}