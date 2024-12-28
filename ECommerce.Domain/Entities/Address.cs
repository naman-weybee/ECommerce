namespace ECommerce.Domain.Entities
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public Address()
        {
        }

        public Address(string street, string city, string state, string postalCode, string country)
        {
            Id = Guid.NewGuid();
            Street = street;
            City = city;
            State = state;
            PostalCode = postalCode;
            Country = country;
        }
    }
}