using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using ECommerce.Infrastructure.Services;

namespace ECommerce.Domain.Aggregates
{
    public class RefreshTokenAggregate : AggregateRoot<RefreshToken>
    {
        private readonly IDomainEventCollector _eventCollector;

        public RefreshToken RefreshToken { get; set; }

        public RefreshTokenAggregate(RefreshToken entity, IDomainEventCollector eventCollector)
            : base(entity, eventCollector)
        {
            RefreshToken = entity;
            _eventCollector = eventCollector;
        }

        public void CreateRefreshToken(Guid userId)
        {
            RefreshToken.CreateRefreshToken(userId);

            EventType = eEventType.RefreshTokenCreated;
            RaiseDomainEvent();
        }

        public void UpdateRefreshToken(RefreshToken refreshToken)
        {
            RefreshToken.UpdateRefreshToken(refreshToken.Id, refreshToken.UserId, refreshToken.Token, refreshToken.IsRevoked, refreshToken.ExpiredDate);

            EventType = eEventType.RefreshTokenUpdated;
            RaiseDomainEvent();
        }

        public void RevokeToken()
        {
            RefreshToken.RevokeToken();

            EventType = eEventType.RefreshTokenRevoked;
            RaiseDomainEvent();
        }

        public void DeleteRefreshToken()
        {
            EventType = eEventType.RefreshTokenDeleted;
            RaiseDomainEvent();
        }

        private void RaiseDomainEvent()
        {
            var domainEvent = new RefreshTokenEvent(RefreshToken.Id, RefreshToken.UserId, RefreshToken.Token, RefreshToken.ExpiredDate, EventType);
            RaiseDomainEvent(domainEvent);
        }
    }
}