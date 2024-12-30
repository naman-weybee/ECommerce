using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<CategoryAggregate, Category> _repository;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<CategoryAggregate, Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync(RequestParams requestParams)
        {
            var items = await _repository.GetAllAsync(requestParams);

            return _mapper.Map<List<CategoryDTO>>(items);
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);

            return _mapper.Map<CategoryDTO>(item);
        }

        public async Task CreateCategoryAsync(CategoryCreateDTO dto)
        {
            var category = _mapper.Map<Category>(dto);
            var aggregate = new CategoryAggregate(category);
            aggregate.CreateCategory(category);

            await _repository.InsertAsync(aggregate);
        }

        public async Task UpdateCategoryAsync(CategoryUpdateDTO dto)
        {
            var category = _mapper.Map<Category>(dto);
            var aggregate = new CategoryAggregate(category);
            aggregate.UpdateCategory(aggregate.Category);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            var aggregate = _mapper.Map<CategoryAggregate>(item);
            aggregate.DeleteCategory();

            await _repository.DeleteAsync(item);
        }
    }
}