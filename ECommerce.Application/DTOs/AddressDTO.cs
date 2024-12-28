using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs
{
    public class AddressDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Street is required."), StringLength(200, ErrorMessage = "Street cannot exceed 200 characters.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "City is required."), StringLength(100, ErrorMessage = "City cannot exceed 100 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required."), StringLength(100, ErrorMessage = "State cannot exceed 100 characters.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Postal Code is required."), StringLength(20, ErrorMessage = "Postal Code cannot exceed 20 characters.")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required."), StringLength(100, ErrorMessage = "Country cannot exceed 100 characters.")]
        public string Country { get; set; }
    }
}