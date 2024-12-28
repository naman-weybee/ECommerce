using ECommerce.Application.DTOs;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetAllOrdersAsync(RequestParams dto);

        Task<OrderDTO> GetOrderByIdAsync(Guid id);

        Task CreateOrderAsync(OrderCreateDTO dto);

        Task UpdateOrderAsync(OrderUpdateDTO dto);

        Task DeleteOrderAsync(Guid id);
    }
}