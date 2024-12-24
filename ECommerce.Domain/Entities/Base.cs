namespace ECommerce.Domain.Entities
{
    public class Base
    {
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? DeletedDate { get; set; }

        public bool IsDeleted { get; set; }

        public void SetDeleted()
        {
            IsDeleted = true;
            DeletedDate = DateTime.UtcNow;
        }
    }
}