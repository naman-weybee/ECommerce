using ECommerce.Application.DTOs;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IOrderItemService
    {
        Task<List<OrderItemDTO>> GetAllOrderItemsAsync(RequestParams requestParams);

        Task<OrderItemDTO> GetOrderItemByIdAsync(Guid id);

        Task<List<OrderItemDTO>> GetOrderItemByOrderIdAsync(Guid orderId);

        Task CreateOrderItemAsync(OrderItemCreateDTO dto);

        Task UpdateOrderItemAsync(OrderItemUpdateDTO dto);

        Task UpdateQuantityAsync(OrderItemQuantityUpdateDTO dto);

        Task UpdateUnitPriceAsync(OrderItemUnitPriceUpdateDTO dto);

        Task DeleteOrderItemAsync(Guid id);
    }
}