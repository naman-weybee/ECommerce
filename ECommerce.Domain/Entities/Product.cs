using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Entities
{
    public class Product : Base
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [Length(1, 500)]
        public string? Description { get; set; }

        public decimal Price { get; set; }

        [Length(3, 3)]
        public string Currency { get; set; }

        public int Stock { get; set; }

        public string? SKU { get; set; }

        public string? Brand { get; set; }

        public Category CategoryId { get; set; }
    }
}