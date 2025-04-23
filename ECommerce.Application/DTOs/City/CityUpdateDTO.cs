namespace ECommerce.Application.DTOs.City
{
    public class CityUpdateDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid StateId { get; set; }
    }
}