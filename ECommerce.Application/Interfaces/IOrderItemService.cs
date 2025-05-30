using ECommerce.Application.DTOs;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IOrderItemService
    {
        Task<List<OrderItemDTO>> GetAllOrderItemsAsync(RequestParams? requestParams = null);

        Task<List<OrderItemDTO>> GetOrderItemsByOrderAsync(Guid orderId, RequestParams? requestParams = null);

        Task<OrderItemDTO> GetOrderItemByIdAsync(Guid id);

        Task UpsertOrderItemAsync(OrderItemUpsertDTO dto);

        Task UpdateQuantityAsync(OrderItemQuantityUpdateDTO dto);

        void UpdateUnitPrice(OrderItemUnitPriceUpdateDTO dto);

        Task DeleteOrderItemAsync(Guid id);
    }
}