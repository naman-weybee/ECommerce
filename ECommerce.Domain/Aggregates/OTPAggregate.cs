using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using ECommerce.Infrastructure.Services;

namespace ECommerce.Domain.Aggregates
{
    public class OTPAggregate : AggregateRoot<OTP>
    {
        private readonly IDomainEventCollector _eventCollector;

        public OTP OTP { get; private set; }

        public OTPAggregate(OTP entity, IDomainEventCollector eventCollector)
             : base(entity, eventCollector)
        {
            OTP = entity;
            _eventCollector = eventCollector;
        }

        public void CreateOTP()
        {
            Entity.CreateOTP(Entity.UserId, Entity.Type);

            EventType = eEventType.OTPGenerated;
            RaiseDomainEvent();
        }

        public void UpdateOTP()
        {
            Entity.UpdateOTP(Entity.Id, Entity.UserId, Entity.Code, Entity.Type, Entity.IsUsed, Entity.Token, Entity.OTPExpiredDate, Entity.TokenExpiredDate);

            EventType = eEventType.OTPUpdated;
            RaiseDomainEvent();
        }

        public void VerifyOTP()
        {
            Entity.VerifyOTP();

            EventType = eEventType.OTPVerified;
            RaiseDomainEvent();
        }

        public void MarkAsUsed()
        {
            Entity.MarkAsUsed();

            EventType = eEventType.OTPUsed;
            RaiseDomainEvent();
        }

        public void DeleteOTP()
        {
            EventType = eEventType.OTPDeleted;
            RaiseDomainEvent();
        }

        private void RaiseDomainEvent()
        {
            var domainEvent = new OTPEvent(Entity.Id, Entity.UserId, Entity.Code, Entity.Type, Entity.IsUsed, Entity.Token, Entity.OTPExpiredDate, Entity.TokenExpiredDate, EventType);
            RaiseDomainEvent(domainEvent);
        }
    }
}