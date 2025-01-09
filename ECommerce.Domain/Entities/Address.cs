using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Entities
{
    public class Address : Base
    {
        public Guid Id { get; set; }

        [StringLength(200)]
        public string Street { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string State { get; set; }

        [StringLength(20)]
        public string PostalCode { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        public void CreateAddress(string street, string city, string state, string postalCode, string country)
        {
            Id = Guid.NewGuid();
            Street = street;
            City = city;
            State = state;
            PostalCode = postalCode;
            Country = country;
            CreatedDate = DateTime.UtcNow;
            UpdatedDate = DateTime.UtcNow;
        }

        public void UpdateAddress(Guid id, string street, string city, string state, string postalCode, string country)
        {
            Id = id;
            Street = street;
            City = city;
            State = state;
            PostalCode = postalCode;
            Country = country;
            UpdatedDate = DateTime.UtcNow;

            StatusUpdated();
        }
    }
}