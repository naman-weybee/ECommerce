namespace ECommerce.Domain.Entities
{
    public class Base
    {
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? DeletedDate { get; set; }

        public bool IsDeleted { get; set; }

        public void StatusUpdated()
        {
            UpdatedDate = DateTime.UtcNow;
        }

        public void StatusDeleted()
        {
            IsDeleted = true;
            DeletedDate = DateTime.UtcNow;
        }
    }
}