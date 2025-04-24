namespace ECommerce.Application.DTOs.State
{
    public class StateUpsertDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid CountryId { get; set; }
    }
}