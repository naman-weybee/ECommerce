using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Events
{
    public class RefreshTokenEvent : BaseEvent
    {
        public Guid RefreshTokenId { get; set; }

        public Guid UserId { get; set; }

        public string Token { get; set; }

        public DateTime ExpiredDate { get; set; }

        public RefreshTokenEvent(Guid refreshTokenId, Guid userId, string token, DateTime expiredDate, eEventType eventType)
        {
            if (refreshTokenId == Guid.Empty)
                throw new ArgumentException("ProductId cannot be empty.", nameof(refreshTokenId));

            RefreshTokenId = refreshTokenId;
            UserId = userId;
            Token = token;
            ExpiredDate = expiredDate;
            EventType = eventType;
        }
    }
}