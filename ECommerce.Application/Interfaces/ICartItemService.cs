using ECommerce.Application.DTOs.CartItem;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface ICartItemService
    {
        Task<List<CartItemDTO>> GetAllCartItemsAsync(RequestParams? requestParams = null);

        Task<List<CartItemDTO>> GetAllCartItemsByUserAsync(Guid userId, RequestParams? requestParams = null);

        Task<CartItemDTO> GetCartItemByIdAsync(Guid id);

        Task<CartItemDTO> GetSpecificCartItemByUserAsync(Guid id, Guid userId);

        Task UpsertCartItemAsync(CartItemUpsertDTO dto);

        Task UpdateQuantityAsync(CartItemQuantityUpdateDTO dto);

        Task UpdateUnitPriceAsync(CartItemUnitPriceUpdateDTO dto);

        Task DeleteCartItemByUserAsync(Guid id, Guid uerId);

        Task DeleteCartItemAsync(Guid id);

        Task ClearCartItemsAsync(Guid uerId);
    }
}