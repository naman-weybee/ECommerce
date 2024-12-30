namespace ECommerce.Application.DTOs
{
    public class CategoryCreateDTO
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public Guid? ParentCategoryId { get; set; }
    }
}