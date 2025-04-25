using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using ECommerce.Infrastructure.Services;

namespace ECommerce.Domain.Aggregates
{
    public class UserAggregate : AggregateRoot<User>
    {
        private readonly IDomainEventCollector _eventCollector;

        public User User { get; set; }

        public UserAggregate(User entity, IDomainEventCollector eventCollector)
            : base(entity, eventCollector)
        {
            User = entity;
            _eventCollector = eventCollector;
        }

        public void CreateUser()
        {
            Entity.CreateUser(Entity.FirstName, Entity.LastName, Entity.Email, Entity.Password, Entity.PhoneNumber, Entity.RoleId, Entity.DateOfBirth, Entity.GenderId, Entity.IsActive, Entity.IsEmailVerified, Entity.IsPhoneNumberVerified, Entity.IsSubscribedToNotifications);

            EventType = eEventType.UserCreated;
            RaiseDomainEvent();
        }

        public void UpdateUser()
        {
            Entity.UpdateUser(Entity.Id, Entity.FirstName, Entity.LastName, Entity.Email, Entity.Password, Entity.PhoneNumber, Entity.RoleId, Entity.DateOfBirth, Entity.GenderId, Entity.IsActive, Entity.IsEmailVerified, Entity.IsPhoneNumberVerified, Entity.IsSubscribedToNotifications);

            EventType = eEventType.UserUpdated;
            RaiseDomainEvent();
        }

        public void ChangeEmail(string email)
        {
            Entity.ChangeEmail(email);

            EventType = eEventType.UserEmailChanged;
            RaiseDomainEvent();
        }

        public void ChangePhoneNumber(string phoneNumber)
        {
            Entity.ChangePhoneNumber(phoneNumber);

            EventType = eEventType.UserPhoneNumberChanged;
            RaiseDomainEvent();
        }

        public void ChangePassword(string password)
        {
            Entity.ChangePassword(password);

            EventType = eEventType.UserPasswordChanged;
            RaiseDomainEvent();
        }

        public void ChangeRole(Guid roleId)
        {
            Entity.ChangeRole(roleId);

            EventType = eEventType.UserRoleChanged;
            RaiseDomainEvent();
        }

        public void ChangeIsActiveStatus(bool isActive)
        {
            Entity.ChangeIsActiveStatus(isActive);

            EventType = eEventType.UserIsActiveStatusChanged;
            RaiseDomainEvent();
        }

        public void EmailVerified()
        {
            Entity.EmailVerified();

            EventType = eEventType.UserEmailVarified;
            RaiseDomainEvent();
        }

        public void ChangeIsPhoneNumberVerifiedStatus(bool isPhoneNumberVerified)
        {
            Entity.ChangeIsPhoneNumberVerifiedStatus(isPhoneNumberVerified);

            EventType = eEventType.UserIsPhoneNumberVerifiedStatusChanged;
            RaiseDomainEvent();
        }

        public void ChangeIsSubscribedToNotificationsStatus(bool isSubscribedToNotifications)
        {
            Entity.ChangeIsSubscribedToNotificationsStatus(isSubscribedToNotifications);

            EventType = eEventType.UserIsSubscribedToNotificationsStatusChanged;
            RaiseDomainEvent();
        }

        public void DeleteUser()
        {
            EventType = eEventType.UserDeleted;
            RaiseDomainEvent();
        }

        private void RaiseDomainEvent()
        {
            var domainEvent = new UserEvent(Entity.Id, Entity.FirstName, Entity.LastName, Entity.Email, Entity.PhoneNumber, Entity.Password, Entity.RoleId, Entity.IsActive, Entity.IsEmailVerified, Entity.IsPhoneNumberVerified, Entity.IsSubscribedToNotifications, EventType);
            RaiseDomainEvent(domainEvent);
        }
    }
}