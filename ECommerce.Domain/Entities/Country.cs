using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Entities
{
    public class Country : Base
    {
        public Guid Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<State> States { get; set; }

        public void CreateCountry(string name, ICollection<State> states)
        {
            Id = Guid.NewGuid();
            Name = name;
            States = states ?? new List<State>();
        }

        public void UpdateCountry(Guid id, string name, ICollection<State> states)
        {
            Id = id;
            Name = name;
            States = states ?? new List<State>();

            StatusUpdated();
        }
    }
}