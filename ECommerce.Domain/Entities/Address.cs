using ECommerce.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Entities
{
    public class Address : Base
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [ForeignKey("Country")]
        public Guid CountryId { get; set; }

        [ForeignKey("State")]
        public Guid StateId { get; set; }

        [ForeignKey("City")]
        public Guid CityId { get; set; }

        [StringLength(20)]
        public string PostalCode { get; set; }

        public eAddressType AdderessType { get; set; }

        [StringLength(500)]
        public string AddressLine { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public virtual User User { get; set; }

        public virtual Country Country { get; set; }

        public virtual State State { get; set; }

        public virtual City City { get; set; }

        public void CreateAddress(Guid userId, string firstName, string lastName, Guid countryId, Guid stateId, Guid cityId, string postalCode, eAddressType adderessType, string addressLine, string phoneNumber)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            CountryId = countryId;
            StateId = stateId;
            CityId = cityId;
            PostalCode = postalCode;
            AdderessType = adderessType;
            AddressLine = addressLine;
            PhoneNumber = phoneNumber;
        }

        public void UpdateAddress(Guid id, Guid userId, string firstName, string lastName, Guid countryId, Guid stateId, Guid cityId, string postalCode, eAddressType adderessType, string addressLine, string phoneNumber)
        {
            Id = id;
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            CountryId = countryId;
            StateId = stateId;
            CityId = cityId;
            PostalCode = postalCode;
            AdderessType = adderessType;
            AddressLine = addressLine;
            PhoneNumber = phoneNumber;

            StatusUpdated();
        }

        public void UpdateAddressType(eAddressType newAddressType)
        {
            AdderessType = newAddressType;

            StatusUpdated();
        }
    }
}