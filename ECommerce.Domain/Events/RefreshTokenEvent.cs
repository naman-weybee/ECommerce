using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Events
{
    public class RefreshTokenEvent : BaseEvent
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Token { get; set; }

        public DateTime ExpiredDate { get; set; }

        public RefreshTokenEvent(Guid id, Guid userId, string token, DateTime expiredDate, eEventType eventType)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException("Token is required");

            Id = id;
            UserId = userId;
            Token = token;
            ExpiredDate = expiredDate;
            EventType = eventType;
        }
    }
}