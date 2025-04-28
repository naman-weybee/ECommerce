using ECommerce.Domain.Enums;

namespace ECommerce.Application.DTOs.OrderStatus
{
    public class OrderStatusDTO
    {
        public eOrderStatus StatusId { get; set; }

        public string Name { get; set; }
    }
}