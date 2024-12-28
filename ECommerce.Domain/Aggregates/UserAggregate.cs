using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;

namespace ECommerce.Domain.Aggregates
{
    public class UserAggregate : AggregateRoot<User>
    {
        public User User { get; set; }

        public UserAggregate(User entity)
            : base(entity)
        {
            User = entity;
        }

        public void CreateUser(User user)
        {
            User.CreateUser(user.FirstName, user.LastName, user.Email, user.Password, user.PhoneNumber, user.RoleId, user.DateOfBirth, user.GenderId, user.IsActive, user.IsEmailVerified, user.IsPhoneNumberVerified, user.IsSubscribedToNotifications);

            EventType = eEventType.UserCreated;
            RaiseDomainEvent();
        }

        public void UpdateUser(User user)
        {
            User.UpdateUser(user.FirstName, user.LastName, user.Email, user.Password, user.PhoneNumber, user.RoleId, user.DateOfBirth, user.GenderId, user.IsActive, user.IsEmailVerified, user.IsPhoneNumberVerified, user.IsSubscribedToNotifications);

            EventType = eEventType.UserUpdated;
            RaiseDomainEvent();
        }

        public void ChangeEmail(string email)
        {
            User.ChangeEmail(email);

            EventType = eEventType.UserEmailChanged;
            RaiseDomainEvent();
        }

        public void ChangePhoneNumber(string phoneNumber)
        {
            User.ChangePhoneNumber(phoneNumber);

            EventType = eEventType.UserPhoneNumberChanged;
            RaiseDomainEvent();
        }

        public void ChangePassword(string password)
        {
            User.ChangePassword(password);

            EventType = eEventType.UserPasswordChanged;
            RaiseDomainEvent();
        }

        public void ChangeRole(Guid roleId)
        {
            User.ChangeRole(roleId);

            EventType = eEventType.UserRoleChanged;
            RaiseDomainEvent();
        }

        public void ChangeIsActiveStatus(bool isActive)
        {
            User.ChangeIsActiveStatus(isActive);

            EventType = eEventType.UserIsActiveStatusChanged;
            RaiseDomainEvent();
        }

        public void ChangeIsEmailVerifiedStatus(bool isEmailVerified)
        {
            User.ChangeIsEmailVerifiedStatus(isEmailVerified);

            EventType = eEventType.UserIsEmailVerifiedStatusChanged;
            RaiseDomainEvent();
        }

        public void ChangeIsPhoneNumberVerifiedStatus(bool isPhoneNumberVerified)
        {
            User.ChangeIsPhoneNumberVerifiedStatus(isPhoneNumberVerified);

            EventType = eEventType.UserIsPhoneNumberVerifiedStatusChanged;
            RaiseDomainEvent();
        }

        public void ChangeIsSubscribedToNotificationsStatus(bool isSubscribedToNotifications)
        {
            User.ChangeIsSubscribedToNotificationsStatus(isSubscribedToNotifications);

            EventType = eEventType.UserIsSubscribedToNotificationsStatusChanged;
            RaiseDomainEvent();
        }

        public void DeleteUser()
        {
            if (User.IsDeleted)
                throw new InvalidOperationException("Cannot delete already deleted User.");

            EventType = eEventType.UserDeleted;
            RaiseDomainEvent();
        }

        private void RaiseDomainEvent()
        {
            var domainEvent = new UserEvent(User.Id, User.Email, User.PhoneNumber, User.Password, User.Role.Name, User.IsActive, User.IsEmailVerified, User.IsPhoneNumberVerified, User.IsSubscribedToNotifications, EventType);
            RaiseDomainEvent(domainEvent);
        }

        public new void ClearDomainEvents()
        {
            base.ClearDomainEvents();
        }
    }
}