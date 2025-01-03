using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;
using Microsoft.EntityFrameworkCore;

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
            var query = _repository.GetDbSet();
            query = query.Include(c => c.Products);

            var items = await _repository.GetAllAsync(requestParams, query);

            return _mapper.Map<List<CategoryDTO>>(items);
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(Guid id)
        {
            var query = _repository.GetDbSet();
            query = query.Include(c => c.Products);

            var item = await _repository.GetByIdAsync(id, query);

            return _mapper.Map<CategoryDTO>(item);
        }

        public async Task CreateCategoryAsync(CategoryCreateDTO dto)
        {
            var item = _mapper.Map<Category>(dto);
            var aggregate = new CategoryAggregate(item);
            aggregate.CreateCategory(item);

            await _repository.InsertAsync(aggregate);
        }

        public async Task UpdateCategoryAsync(CategoryUpdateDTO dto)
        {
            var item = _mapper.Map<Category>(dto);
            var aggregate = new CategoryAggregate(item);
            aggregate.UpdateCategory(aggregate.Category);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task AddSubCategoryAsync(Guid id, CategoryCreateDTO dto)
        {
            var item = await _repository.GetByIdAsync(id);
            var aggregate = new CategoryAggregate(item);

            var subCategory = _mapper.Map<Category>(dto);
            aggregate.AddSubCategory(subCategory);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task RemoveSubCategoryAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item.ParentCategoryId == null)
                throw new Exception("Provided Category is not Sub Caegory, Its Parent Category.");

            var aggregate = _mapper.Map<CategoryAggregate>(item);
            aggregate.RemoveSubCategory();

            await _repository.DeleteAsync(item);
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