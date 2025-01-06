using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Entities
{
    public class Gender : Base
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public Gender()
        {
        }

        public Gender(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public void CreateGender(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public void UpdateGender(Guid id, string name)
        {
            Id = id;
            Name = name;
            StatusUpdated();
        }
    }
}