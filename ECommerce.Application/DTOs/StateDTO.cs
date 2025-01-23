namespace ECommerce.Application.DTOs
{
    public class StateDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid CountryId { get; set; }

        public virtual ICollection<CityDTO> Cities { get; set; }
    }
}