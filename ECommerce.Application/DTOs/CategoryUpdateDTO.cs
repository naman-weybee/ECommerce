﻿namespace ECommerce.Application.DTOs
{
    public class CategoryUpdateDTO
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public Guid? ParentCategoryId { get; set; }
    }
}