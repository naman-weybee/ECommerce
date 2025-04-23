using ECommerce.Domain.Enums;

namespace ECommerce.Application.DTOs.OTP
{
    public class OTPCreateDTO
    {
        public Guid UserId { get; set; }

        public eOTPType Type { get; set; }
    }
}