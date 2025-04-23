using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.Order;
using ECommerce.Application.DTOs.OrderItem;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetAllOrdersAsync(RequestParams? requestParams = null, bool useQuery = false);

        Task<List<OrderDTO>> GetAllOrdersByUserAsync(Guid userId, RequestParams? requestParams = null, bool useQuery = false);

        Task<List<OrderDTO>> GetAllRecentOrdersAsync(RequestParams? requestParams = null, bool useQuery = false);

        Task<List<OrderDTO>> GetAllRecentOrdersByUserAsync(Guid userId, RequestParams? requestParams = null, bool useQuery = false);

        Task<OrderDTO> GetOrderByIdAsync(Guid id, bool useQuery = false);

        Task<OrderDTO> GetSpecificOrderByUserAsync(Guid id, Guid userId, bool useQuery = false);

        Task CreateOrderAsync(OrderCreateFromCartDTO dto);

        Task UpdateOrderAsync(OrderUpdateDTO dto);

        Task UpdateOrderStatusAsync(OrderUpdateStatusDTO dto);

        Task DeleteOrderAsync(Guid id);
    }
}