using ECommerce.Application.DTOs.Category;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllCategoriesAsync(RequestParams? requestParams = null, bool isInclude = false);

        Task<CategoryDTO> GetCategoryByIdAsync(Guid id, bool isInclude = false);

        Task UpsertCategoryAsync(CategoryUpsertDTO dto);

        Task AddSubCategoryAsync(Guid id, CategoryUpsertDTO dto);

        Task RemoveSubCategoryAsync(Guid id);

        Task DeleteCategoryAsync(Guid id);
    }
}