using MediatR;

namespace ECommerce.Domain.Events
{
    public class CategoryEvent : INotification
    {
        public Guid CategoryId { get; set; }

        public string Name { get; set; }

        public Guid? ParentCategoryId { get; set; }

        public CategoryEvent(Guid categoryId, string name, Guid? parentCategoryId)
        {
            CategoryId = categoryId;
            Name = name;
            ParentCategoryId = parentCategoryId;
        }
    }
}