using ECommerce.Domain.Enums;

namespace ECommerce.Application.DTOs
{
    public class OrderUpdateStatusDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public eOrderStatus OrderStatus { get; set; }
    }
}