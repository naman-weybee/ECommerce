using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Entities
{
    public class City : Base
    {
        public Guid Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [ForeignKey("State")]
        public Guid StateId { get; set; }

        public virtual State State { get; set; }

        public void CreateCity(string name, Guid stateId)
        {
            Id = Guid.NewGuid();
            Name = name;
            StateId = stateId;

            StatusUpdated();
        }

        public void UpdateCity(Guid id, string name, Guid stateId)
        {
            Id = id;
            Name = name;
            StateId = stateId;

            StatusUpdated();
        }
    }
}