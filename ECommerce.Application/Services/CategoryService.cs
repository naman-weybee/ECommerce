using AutoMapper;
using ECommerce.Application.DTOs.Category;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Services;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repository;
        private readonly IServiceHelper<Category> _serviceHelper;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public CategoryService(IRepository<Category> repository, IServiceHelper<Category> serviceHelper, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _serviceHelper = serviceHelper;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync(RequestParams? requestParams = null, bool useQuery = false)
        {
            var query = useQuery
                ? _repository.GetQuery().Include(c => c.Products)!
                : null!;

            var items = await _serviceHelper.GetAllAsync(requestParams, query);

            return _mapper.Map<List<CategoryDTO>>(items);
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(Guid id, bool useQuery = false)
        {
            var query = useQuery
                ? _repository.GetQuery().Include(c => c.Products)!
                : null!;

            var item = await _serviceHelper.GetByIdAsync(id, query);

            return _mapper.Map<CategoryDTO>(item);
        }

        public async Task CreateCategoryAsync(CategoryCreateDTO dto)
        {
            var item = _mapper.Map<Category>(dto);
            var aggregate = new CategoryAggregate(item, _eventCollector);
            aggregate.CreateCategory(item);

            await _repository.InsertAsync(aggregate.Entity);
        }

        public async Task UpdateCategoryAsync(CategoryUpdateDTO dto)
        {
            var item = _mapper.Map<Category>(dto);
            var aggregate = new CategoryAggregate(item, _eventCollector);
            aggregate.UpdateCategory(aggregate.Category);

            await _repository.UpdateAsync(aggregate.Entity);
        }

        public async Task AddSubCategoryAsync(Guid id, CategoryCreateDTO dto)
        {
            var item = await _serviceHelper.GetByIdAsync(id);
            var aggregate = new CategoryAggregate(item, _eventCollector);

            var subCategory = _mapper.Map<Category>(dto);
            aggregate.AddSubCategory(subCategory);

            await _repository.UpdateAsync(aggregate.Entity);
        }

        public async Task RemoveSubCategoryAsync(Guid id)
        {
            var item = await _serviceHelper.GetByIdAsync(id);
            if (item.ParentCategoryId == null)
                throw new Exception("Provided Category is not Sub Caegory, Its Parent Category.");

            var aggregate = new CategoryAggregate(item, _eventCollector);
            aggregate.RemoveSubCategory();

            await _repository.DeleteAsync(item);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            var item = await _serviceHelper.GetByIdAsync(id);
            var aggregate = new CategoryAggregate(item, _eventCollector);
            aggregate.DeleteCategory();

            await _repository.DeleteAsync(item);
        }
    }
}