using ECommerce.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs
{
    public class ProductPriceChangeDTO
    {
        [Required]
        public Guid Id { get; set; }

        public Money Price { get; set; }
    }
}