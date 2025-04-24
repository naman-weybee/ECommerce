using AutoMapper;
using ECommerce.Application.DTOs.Product;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.DomainServices.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Services;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Services
{
    public class ProductService : IProductService, IProductDomainService
    {
        private readonly IRepository<Product> _repository;
        private readonly IServiceHelper<Product> _serviceHelper;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public ProductService(IRepository<Product> repository, IServiceHelper<Product> serviceHelper, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _serviceHelper = serviceHelper;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<ProductDTO>> GetAllProductsAsync(RequestParams? requestParams = null)
        {
            var items = await _serviceHelper.GetAllAsync(requestParams);

            return _mapper.Map<List<ProductDTO>>(items);
        }

        public async Task<ProductDTO> GetProductByIdAsync(Guid id)
        {
            var item = await _serviceHelper.GetByIdAsync(id);

            return _mapper.Map<ProductDTO>(item);
        }

        public async Task<int> GetProductStockByIdAsync(Guid id)
        {
            var item = await _serviceHelper.GetByIdAsync(id);

            return item.Stock;
        }

        public async Task CreateProductAsync(ProductCreateDTO dto)
        {
            var item = _mapper.Map<Product>(dto);
            var aggregate = new ProductAggregate(item, _eventCollector);
            aggregate.CreateProduct(item);

            await _repository.InsertAsync(aggregate.Entity);
        }

        public async Task UpdateProductAsync(ProductUpdateDTO dto)
        {
            var item = _mapper.Map<Product>(dto);
            var aggregate = new ProductAggregate(item, _eventCollector);
            aggregate.UpdateProduct(aggregate.Product);

            await _repository.UpdateAsync(aggregate.Entity);
        }

        public async Task ProductStockChangeAsync(Guid id, int quantity, bool isIncrease)
        {
            var item = await _serviceHelper.GetByIdAsync(id);
            var aggregate = new ProductAggregate(item, _eventCollector);

            if (isIncrease)
                aggregate.IncreaseStock(quantity);
            else
                aggregate.DecreaseStock(quantity);

            await _repository.UpdateAsync(aggregate.Entity);
        }

        public async Task ProductPriceChangeAsync(ProductPriceChangeDTO dto)
        {
            var item = await _serviceHelper.GetByIdAsync(dto.Id);
            var aggregate = new ProductAggregate(item, _eventCollector);
            aggregate.ChangePrice(dto.Price);

            await _repository.UpdateAsync(aggregate.Entity);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var item = await _serviceHelper.GetByIdAsync(id);
            var aggregate = new ProductAggregate(item, _eventCollector);
            aggregate.DeleteProduct();

            await _repository.DeleteAsync(item);
        }
    }
}