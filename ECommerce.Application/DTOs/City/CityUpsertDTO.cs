namespace ECommerce.Application.DTOs.City
{
    public class CityUpsertDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid StateId { get; set; }
    }
}