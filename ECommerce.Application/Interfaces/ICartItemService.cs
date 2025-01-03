using ECommerce.Application.DTOs;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface ICartItemService
    {
        Task<List<CartItemDTO>> GetAllCartItemsAsync(RequestParams requestParams);

        Task<CartItemDTO> GetCartItemByIdAsync(Guid id);

        Task CreateCartItemAsync(CartItemCreateDTO dto);

        Task UpdateCartItemAsync(CartItemUpdateDTO dto);

        Task UpdateQuantityAsync(CartItemQuantityUpdateDTO dto);

        Task UpdateUnitPriceAsync(CartItemUnitPriceUpdateDTO dto);

        Task DeleteCartItemAsync(Guid id);
    }
}