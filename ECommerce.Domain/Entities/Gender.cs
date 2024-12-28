using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Entities
{
    public class Gender
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
    }
}