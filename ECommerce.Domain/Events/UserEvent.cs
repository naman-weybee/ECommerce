using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Events
{
    public class UserEvent : BaseEvent
    {
        public Guid UserId { get; }

        public string Email { get; }

        public string PhoneNumber { get; }

        public string Password { get; }

        public string Role { get; }

        public bool IsActive { get; set; } = true;

        public bool IsEmailVerified { get; set; }

        public bool IsPhoneNumberVerified { get; set; }

        public bool IsSubscribedToNotifications { get; set; }

        public UserEvent(Guid userId, string email, string phoneNumber, string password, string role, bool isActive, bool isEmailVerified, bool isPhoneNumberVerified, bool isSubscribedToNotifications, eEventType eventType)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId cannot be empty.", nameof(userId));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty.", nameof(email));

            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("PhoneNumber cannot be empty.", nameof(phoneNumber));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty.", nameof(password));

            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentException("Role cannot be empty.", nameof(role));

            UserId = userId;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            Role = role;
            IsActive = isActive;
            IsEmailVerified = isEmailVerified;
            IsPhoneNumberVerified = isPhoneNumberVerified;
            IsSubscribedToNotifications = isSubscribedToNotifications;
            EventType = eventType;
        }
    }
}