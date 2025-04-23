using ECommerce.Application.DTOs.CartItem;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface ICartItemService
    {
        Task<List<CartItemDTO>> GetAllCartItemsAsync(RequestParams? requestParams = null);

        Task<List<CartItemDTO>> GetAllCartItemsByUserAsync(Guid userId, RequestParams? requestParams = null);

        Task<CartItemDTO> GetCartItemByIdAsync(Guid id);

        Task<CartItemDTO> GetSpecificCartItemsByUserAsync(Guid id, Guid userId);

        Task CreateCartItemAsync(CartItemCreateDTO dto);

        Task UpdateCartItemAsync(CartItemUpdateDTO dto);

        Task UpdateQuantityAsync(CartItemQuantityUpdateDTO dto);

        Task UpdateUnitPriceAsync(CartItemUnitPriceUpdateDTO dto);

        Task DeleteCartItemAsync(Guid id, Guid uerId);

        Task ClearCartItemsAsync(Guid uerId);
    }
}