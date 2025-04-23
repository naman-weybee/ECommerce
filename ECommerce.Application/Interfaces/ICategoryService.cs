using ECommerce.Application.DTOs;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllCategoriesAsync(RequestParams? requestParams = null, bool isInclude = false);

        Task<CategoryDTO> GetCategoryByIdAsync(Guid id, bool isInclude = false);

        Task CreateCategoryAsync(CategoryCreateDTO dto);

        Task UpdateCategoryAsync(CategoryUpdateDTO dto);

        Task AddSubCategoryAsync(Guid id, CategoryCreateDTO dto);

        Task RemoveSubCategoryAsync(Guid id);

        Task DeleteCategoryAsync(Guid id);
    }
}