using ECommerce.Application.DTOs.Role;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs.User
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public Guid RoleId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        public Guid GenderId { get; set; }

        public bool IsActive { get; set; }

        public bool IsEmailVerified { get; set; }

        public bool IsPhoneNumberVerified { get; set; }

        public bool IsSubscribedToNotifications { get; set; }

        public RoleDTO Role { get; set; }

        public string? EmailVerificationToken { get; set; }
    }
}