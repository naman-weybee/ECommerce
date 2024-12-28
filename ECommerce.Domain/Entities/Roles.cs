using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Entities
{
    public class Roles
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}