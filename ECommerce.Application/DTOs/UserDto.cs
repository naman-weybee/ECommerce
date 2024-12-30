using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(100, ErrorMessage = "First Name cannot exceed 100 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(100, ErrorMessage = "Last Name cannot exceed 100 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
        [StringLength(256, ErrorMessage = "Email cannot exceed 256 characters.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Please provide a valid phone number.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "RoleId is required.")]
        public Guid RoleId { get; set; }

        [Required(ErrorMessage = "AddressId is required.")]
        public Guid AddressId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "GenderId is required.")]
        public Guid GenderId { get; set; }

        public bool IsActive { get; set; }

        public bool IsEmailVerified { get; set; }

        public bool IsPhoneNumberVerified { get; set; }

        public bool IsSubscribedToNotifications { get; set; }
    }
}