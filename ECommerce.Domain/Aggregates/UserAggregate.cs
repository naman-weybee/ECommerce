using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using MediatR;

namespace ECommerce.Domain.Aggregates
{
    public class UserAggregate : AggregateRoot<User>
    {
        private readonly IMediator _mediator;

        public User User { get; set; }

        public UserAggregate(User entity, IMediator mediator)
            : base(entity, mediator)
        {
            User = entity;
            _mediator = mediator;
        }

        public async Task CreateUser(User user)
        {
            User.CreateUser(user.FirstName, user.LastName, user.Email, user.Password, user.PhoneNumber, user.RoleId, user.DateOfBirth, user.GenderId, user.AddressId, user.IsActive, user.IsEmailVerified, user.IsPhoneNumberVerified, user.IsSubscribedToNotifications);

            EventType = eEventType.UserCreated;
            await RaiseDomainEvent();
        }

        public async Task UpdateUser(User user)
        {
            User.UpdateUser(user.Id, user.FirstName, user.LastName, user.Email, user.Password, user.PhoneNumber, user.RoleId, user.DateOfBirth, user.GenderId, user.AddressId, user.IsActive, user.IsEmailVerified, user.IsPhoneNumberVerified, user.IsSubscribedToNotifications);

            EventType = eEventType.UserUpdated;
            await RaiseDomainEvent();
        }

        public async Task ChangeEmail(string email)
        {
            User.ChangeEmail(email);

            EventType = eEventType.UserEmailChanged;
            await RaiseDomainEvent();
        }

        public async Task ChangePhoneNumber(string phoneNumber)
        {
            User.ChangePhoneNumber(phoneNumber);

            EventType = eEventType.UserPhoneNumberChanged;
            await RaiseDomainEvent();
        }

        public async Task ChangePassword(string password)
        {
            User.ChangePassword(password);

            EventType = eEventType.UserPasswordChanged;
            await RaiseDomainEvent();
        }

        public async Task ChangeRole(Guid roleId)
        {
            User.ChangeRole(roleId);

            EventType = eEventType.UserRoleChanged;
            await RaiseDomainEvent();
        }

        public async Task ChangeAddress(Guid roleId)
        {
            User.ChangeAddress(roleId);

            EventType = eEventType.UserAddressChanged;
            await RaiseDomainEvent();
        }

        public async Task ChangeIsActiveStatus(bool isActive)
        {
            User.ChangeIsActiveStatus(isActive);

            EventType = eEventType.UserIsActiveStatusChanged;
            await RaiseDomainEvent();
        }

        public async Task ChangeIsEmailVerifiedStatus(bool isEmailVerified)
        {
            User.ChangeIsEmailVerifiedStatus(isEmailVerified);

            EventType = eEventType.UserIsEmailVerifiedStatusChanged;
            await RaiseDomainEvent();
        }

        public async Task ChangeIsPhoneNumberVerifiedStatus(bool isPhoneNumberVerified)
        {
            User.ChangeIsPhoneNumberVerifiedStatus(isPhoneNumberVerified);

            EventType = eEventType.UserIsPhoneNumberVerifiedStatusChanged;
            await RaiseDomainEvent();
        }

        public async Task ChangeIsSubscribedToNotificationsStatus(bool isSubscribedToNotifications)
        {
            User.ChangeIsSubscribedToNotificationsStatus(isSubscribedToNotifications);

            EventType = eEventType.UserIsSubscribedToNotificationsStatusChanged;
            await RaiseDomainEvent();
        }

        public async Task DeleteUser()
        {
            EventType = eEventType.UserDeleted;
            await RaiseDomainEvent();
        }

        private async Task RaiseDomainEvent()
        {
            var domainEvent = new UserEvent(User.Id, User.FirstName, User.LastName, User.Email, User.PhoneNumber, User.Password, User.RoleId, User.IsActive, User.IsEmailVerified, User.IsPhoneNumberVerified, User.IsSubscribedToNotifications, EventType);
            await RaiseDomainEvent(domainEvent);
        }

        public new void ClearDomainEvents()
        {
            base.ClearDomainEvents();
        }
    }
}