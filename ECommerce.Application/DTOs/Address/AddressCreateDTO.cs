using ECommerce.Domain.Enums;

namespace ECommerce.Application.DTOs.Address
{
    public class AddressCreateDTO
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid CountryId { get; set; }

        public Guid StateId { get; set; }

        public Guid CityId { get; set; }

        public string PostalCode { get; set; }

        public eAddressType AdderessType { get; set; }

        public string AddressLine { get; set; }

        public string PhoneNumber { get; set; }
    }
}