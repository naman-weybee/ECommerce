using ECommerce.Domain.Enums;

namespace ECommerce.Application.DTOs.OTP
{
    public class OTPDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Code { get; set; }

        public eOTPType Type { get; set; }

        public bool IsUsed { get; set; }

        public string? Token { get; set; }

        public DateTime OTPExpiredDate { get; set; }

        public DateTime? TokenExpiredDate { get; set; }
    }
}