using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Entities
{
    public class State : Base
    {
        public Guid Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [ForeignKey("Country")]
        public Guid CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<City> Cities { get; set; }

        public void CreateState(string name, Guid countryId, ICollection<City> cities)
        {
            Id = Guid.NewGuid();
            Name = name;
            CountryId = countryId;
            Cities = cities ?? new List<City>();
        }

        public void UpdateState(Guid id, string name, Guid countryId, ICollection<City> cities)
        {
            Id = id;
            Name = name;
            CountryId = countryId;
            Cities = cities ?? new List<City>();

            StatusUpdated();
        }
    }
}