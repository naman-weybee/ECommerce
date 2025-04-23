using ECommerce.Domain.Enums;

namespace ECommerce.Application.DTOs.OTP
{
    public class OTPCreateFromEmailDTO
    {
        public string Email { get; set; }

        public eOTPType Type { get; set; }
    }
}