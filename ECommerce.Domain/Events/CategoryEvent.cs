using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Events
{
    public class CategoryEvent : BaseEvent
    {
        public Guid CategoryId { get; set; }

        public string Name { get; set; }

        public Guid? ParentCategoryId { get; set; }

        public CategoryEvent(Guid categoryId, string name, Guid? parentCategoryId, eEventType eventType)
        {
            if (categoryId == Guid.Empty)
                throw new ArgumentException("CategoryId cannot be empty.", nameof(categoryId));

            CategoryId = categoryId;
            Name = name;
            ParentCategoryId = parentCategoryId;
            EventType = eventType;
        }
    }
}