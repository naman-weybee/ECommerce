using ECommerce.Application.DTOs.OrderStatus;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IOrderStatusService
    {
        Task<List<OrderStatusDTO>> GetAllOrderStatusAsync(RequestParams requestParams);
    }
}