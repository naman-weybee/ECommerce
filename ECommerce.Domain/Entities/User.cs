using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Entities
{
    public class User : IdentityUser
    {
        public Guid Id { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        public string Password { get; set; }

        [EmailAddress, StringLength(256)]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [ForeignKey("Role")]
        public Guid RoleId { get; set; }

        public Role Role { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [ForeignKey("Gender")]
        public Guid GenderId { get; set; }

        public Gender Gender { get; set; }

        [ForeignKey("Address")]
        public Guid AddressId { get; set; }

        public Address Address { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsEmailVerified { get; set; }

        public bool IsPhoneNumberVerified { get; set; }

        public bool IsSubscribedToNotifications { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedDate { get; set; }

        public DateTime? DeletedDate { get; set; }

        public bool IsDeleted { get; set; }

        public User()
        {
        }

        public User(string firstName, string lastName, string email, string password, string phoneNumber, Guid roleId, DateTime? dateOfBirth, Guid genderId, Guid addressId, bool isActive, bool isEmailVerified, bool isPhoneNumberVerified, bool isSubscribedToNotifications)
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
            AddressId = addressId;
            IsActive = isActive;
            IsEmailVerified = isEmailVerified;
            IsPhoneNumberVerified = isPhoneNumberVerified;
            IsSubscribedToNotifications = isSubscribedToNotifications;
        }

        public void CreateUser(string firstName, string lastName, string email, string password, string phoneNumber, Guid roleId, DateTime? dateOfBirth, Guid genderId, Guid addressId, bool isActive, bool isEmailVerified, bool isPhoneNumberVerified, bool isSubscribedToNotifications)
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
            AddressId = addressId;
            IsActive = isActive;
            IsEmailVerified = isEmailVerified;
            IsPhoneNumberVerified = isPhoneNumberVerified;
            IsSubscribedToNotifications = isSubscribedToNotifications;
        }

        public void UpdateUser(string firstName, string lastName, string email, string password, string phoneNumber, Guid roleId, DateTime? dateOfBirth, Guid genderId, Guid addressId, bool isActive, bool isEmailVerified, bool isPhoneNumberVerified, bool isSubscribedToNotifications)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            RoleId = roleId;
            DateOfBirth = dateOfBirth;
            GenderId = genderId;
            AddressId = addressId;
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

        public void ChangeAddress(Guid addressId)
        {
            AddressId = addressId;
            StatusUpdated();
        }

        public void ChangeIsActiveStatus(bool isActive)
        {
            IsActive = isActive;
            StatusUpdated();
        }

        public void ChangeIsEmailVerifiedStatus(bool isEmailVerified)
        {
            IsEmailVerified = isEmailVerified;
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

        public void StatusUpdated()
        {
            UpdatedDate = DateTime.UtcNow;
        }

        public void StatusDeleted()
        {
            IsDeleted = true;
            DeletedDate = DateTime.UtcNow;
        }
    }
}