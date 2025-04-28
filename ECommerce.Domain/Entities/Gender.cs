using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Entities
{
    public class Gender
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public void CreateGender(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public void UpdateGender(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}