using ECommerce.Domain.Enums;

namespace ECommerce.Application.DTOs
{
    public class OTPCreateDTO
    {
        public Guid UserId { get; set; }

        public eOTPType Type { get; set; }
    }
}