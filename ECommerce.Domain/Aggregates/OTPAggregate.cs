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

        public void CreateOTP(OTP otp)
        {
            OTP.CreateOTP(otp.UserId, otp.Type);

            EventType = eEventType.OTPGenerated;
            RaiseDomainEvent();
        }

        public void UpdateOTP(OTP otp)
        {
            OTP.UpdateOTP(otp.Id, otp.UserId, otp.Code, otp.Type, otp.IsUsed, otp.Token, otp.OTPExpiredDate, otp.TokenExpiredDate);

            EventType = eEventType.OTPUpdated;
            RaiseDomainEvent();
        }

        public void VerifyOTP()
        {
            OTP.VerifyOTP();

            EventType = eEventType.OTPVerified;
            RaiseDomainEvent();
        }

        public void MarkAsUsed()
        {
            OTP.MarkAsUsed();

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
            var domainEvent = new OTPEvent(OTP.Id, OTP.UserId, OTP.Code, OTP.Type, OTP.IsUsed, OTP.Token, OTP.OTPExpiredDate, OTP.TokenExpiredDate, EventType);
            RaiseDomainEvent(domainEvent);
        }
    }
}