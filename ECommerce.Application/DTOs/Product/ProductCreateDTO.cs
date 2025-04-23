using ECommerce.Domain.ValueObjects;

namespace ECommerce.Application.DTOs.Product
{
    public class ProductCreateDTO
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public Money Price { get; set; }

        public Currency Currency { get; set; }

        public int Stock { get; set; }

        public string? SKU { get; set; }

        public string? Brand { get; set; }

        public Guid CategoryId { get; set; }
    }
}