using ECommerce.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs.Product
{
    public class ProductUpdateDTO
    {
        [Required]
        public Guid Id { get; set; }

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