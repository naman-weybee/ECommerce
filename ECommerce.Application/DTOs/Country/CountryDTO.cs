using ECommerce.Application.DTOs.State;

namespace ECommerce.Application.DTOs.Country
{
    public class CountryDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<StateDTO> States { get; set; }
    }
}