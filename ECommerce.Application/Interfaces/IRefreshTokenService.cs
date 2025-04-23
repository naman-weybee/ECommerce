using ECommerce.Application.DTOs;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<List<RefreshTokenDTO>> GetAllRefreshTokensAsync(RequestParams? requestParams = null);

        Task<List<RefreshTokenDTO>> GetAllRefreshTokensByUserAsync(Guid userId, RequestParams? requestParams = null);

        Task<RefreshTokenDTO> GetRefreshTokenByIdAsync(Guid id);

        Task<RefreshTokenDTO> GetSpecificRefreshTokenByUserAsync(Guid id, Guid userId);

        Task<RefreshTokenDTO> CreateRefreshTokenAsync(RefreshTokenCreateDTO dto);

        Task UpdateRefreshTokenAsync(RefreshTokenUpdateDTO dto);

        Task DeleteRefreshTokenAsync(Guid id, Guid userId);

        Task RevokeRefreshTokenAsync(RevokeRefreshTokenDTO dto);
    }
}