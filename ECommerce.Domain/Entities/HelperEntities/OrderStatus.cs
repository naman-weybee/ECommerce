using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities.HelperEntities
{
    public class OrderStatus
    {
        public eOrderStatus StatusId { get; set; }

        public string Name { get; set; }
    }
}