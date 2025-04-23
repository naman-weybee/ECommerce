using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs.Product
{
    public class ProductStockChangeDTO
    {
        [Required]
        public Guid Id { get; set; }

        public int Quantity { get; set; }
    }
}