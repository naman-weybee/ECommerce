namespace ECommerce.Application.DTOs.State
{
    public class StateUpdateDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid CountryId { get; set; }
    }
}