using ECommerce.Domain.Enums;

namespace ECommerce.Application.DTOs
{
    public class AddressTypeUpdateDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public eAddressType AdderessType { get; set; }
    }
}