namespace ECommerce.Application.DTOs
{
    public class CountryDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<StateDTO> States { get; set; }
    }
}