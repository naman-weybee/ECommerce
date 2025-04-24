namespace ECommerce.Application.DTOs.Category
{
    public class CategoryUpsertDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public Guid? ParentCategoryId { get; set; }
    }
}