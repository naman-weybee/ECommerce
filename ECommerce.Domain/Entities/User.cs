using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Entities
{
    public class User : Base
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        public string Password { get; set; }

        [EmailAddress, StringLength(256)]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [ForeignKey("Role")]
        public Guid RoleId { get; set; }

        public virtual Role Role { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [ForeignKey("Gender")]
        public Guid GenderId { get; set; }

        public virtual Gender Gender { get; set; }

        public bool IsActive { get; set; } = true;

        public string? EmailVerificationToken { get; set; }

        public bool IsEmailVerified { get; set; }

        public bool IsPhoneNumberVerified { get; set; }

        public bool IsSubscribedToNotifications { get; set; }

        public void CreateUser(string firstName, string lastName, string email, string password, string phoneNumber, Guid roleId, DateTime? dateOfBirth, Guid genderId, bool isActive, bool isEmailVerified, bool isPhoneNumberVerified, bool isSubscribedToNotifications)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            RoleId = roleId;
            DateOfBirth = dateOfBirth;
            GenderId = genderId;
            IsActive = isActive;
            IsEmailVerified = isEmailVerified;
            EmailVerificationToken = Guid.NewGuid().ToString();
            IsPhoneNumberVerified = isPhoneNumberVerified;
            IsSubscribedToNotifications = isSubscribedToNotifications;
        }

        public void UpdateUser(Guid id, string firstName, string lastName, string email, string password, string phoneNumber, Guid roleId, DateTime? dateOfBirth, Guid genderId, bool isActive, bool isEmailVerified, bool isPhoneNumberVerified, bool isSubscribedToNotifications)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            RoleId = roleId;
            DateOfBirth = dateOfBirth;
            GenderId = genderId;
            IsActive = isActive;
            IsEmailVerified = isEmailVerified;
            IsPhoneNumberVerified = isPhoneNumberVerified;
            IsSubscribedToNotifications = isSubscribedToNotifications;

            StatusUpdated();
        }

        public void ChangeEmail(string email)
        {
            Email = email;
            StatusUpdated();
        }

        public void ChangePhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
            StatusUpdated();
        }

        public void ChangePassword(string password)
        {
            Password = password;
            StatusUpdated();
        }

        public void ChangeRole(Guid roleId)
        {
            RoleId = roleId;
            StatusUpdated();
        }

        public void ChangeIsActiveStatus(bool isActive)
        {
            IsActive = isActive;
            StatusUpdated();
        }

        public void EmailVerified()
        {
            EmailVerificationToken = null;
            IsEmailVerified = true;

            StatusUpdated();
        }

        public void ChangeIsPhoneNumberVerifiedStatus(bool isPhoneNumberVerified)
        {
            IsPhoneNumberVerified = isPhoneNumberVerified;
            StatusUpdated();
        }

        public void ChangeIsSubscribedToNotificationsStatus(bool isSubscribedToNotifications)
        {
            IsSubscribedToNotifications = isSubscribedToNotifications;
            StatusUpdated();
        }

        public void ResendEmailVerificationToken()
        {
            EmailVerificationToken = Guid.NewGuid().ToString();
            StatusUpdated();
        }
    }
}