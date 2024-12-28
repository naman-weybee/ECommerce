using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs
{
    public class AddressUpdateDTO
    {
        [Required(ErrorMessage = "Address ID is required.")]
        public Guid Id { get; set; }

        [StringLength(200, ErrorMessage = "Street cannot exceed 200 characters.")]
        public string Street { get; set; }

        [StringLength(100, ErrorMessage = "City cannot exceed 100 characters.")]
        public string City { get; set; }

        [StringLength(100, ErrorMessage = "State cannot exceed 100 characters.")]
        public string State { get; set; }

        [StringLength(20, ErrorMessage = "Postal Code cannot exceed 20 characters.")]
        public string PostalCode { get; set; }

        [StringLength(100, ErrorMessage = "Country cannot exceed 100 characters.")]
        public string Country { get; set; }
    }
}