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

        public Roles Role { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [ForeignKey("Gender")]
        public Guid GenderId { get; set; }

        public Gender Gender { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsEmailVerified { get; set; }

        public bool IsPhoneNumberVerified { get; set; }

        public bool IsSubscribedToNotifications { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? DeletedDate { get; set; }

        public bool IsDeleted { get; set; }

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