using ECommerce.Application.DTOs.City;

namespace ECommerce.Application.DTOs.State
{
    public class StateDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid CountryId { get; set; }

        public virtual ICollection<CityDTO> Cities { get; set; }
    }
}