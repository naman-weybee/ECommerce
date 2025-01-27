using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Events
{
    public class OTPEvent : BaseEvent
    {
        public Guid OTPId { get; set; }

        public Guid UserId { get; set; }

        public string Code { get; set; }

        public eOTPType Type { get; set; }

        public bool IsUsed { get; set; }

        public string? Token { get; set; }

        public DateTime OTPExpiredDate { get; set; }

        public DateTime? TokenExpiredDate { get; set; }

        public OTPEvent(Guid otpId, Guid userId, string code, eOTPType type, bool isUsed, string? token, DateTime oTPExpiredDate, DateTime? tokenExpiredDate, eEventType eventType)
        {
            if (otpId == Guid.Empty)
                throw new ArgumentException("OTPId cannot be empty.", nameof(otpId));

            if (userId == Guid.Empty)
                throw new ArgumentException("UserId cannot be empty.", nameof(userId));

            OTPId = otpId;
            UserId = userId;
            Code = code;
            Type = type;
            IsUsed = isUsed;
            Token = token;
            OTPExpiredDate = oTPExpiredDate;
            TokenExpiredDate = tokenExpiredDate;
            EventType = eventType;
        }
    }
}