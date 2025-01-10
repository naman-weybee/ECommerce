using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.DomainServices;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Services;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Services
{
    public class ProductService : IProductService, IIProductDomainService
    {
        private readonly IRepository<ProductAggregate, Product> _repository;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public ProductService(IRepository<ProductAggregate, Product> repository, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<ProductDTO>> GetAllProductsAsync(RequestParams requestParams)
        {
            var items = await _repository.GetAllAsync(requestParams);
            return _mapper.Map<List<ProductDTO>>(items);
        }

        public async Task<ProductDTO> GetProductByIdAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            return _mapper.Map<ProductDTO>(item);
        }

        public async Task<int> GetProductStockByIdAsync(Guid id)
        {
            var item = await GetProductByIdAsync(id);
            return item.Stock;
        }

        public async Task CreateProductAsync(ProductCreateDTO dto)
        {
            var item = _mapper.Map<Product>(dto);
            var aggregate = new ProductAggregate(item, _eventCollector);
            aggregate.CreateProduct(item);

            await _repository.InsertAsync(aggregate);
        }

        public async Task UpdateProductAsync(ProductUpdateDTO dto)
        {
            var item = _mapper.Map<Product>(dto);
            var aggregate = new ProductAggregate(item, _eventCollector);
            aggregate.UpdateProduct(aggregate.Product);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task ProductStockChangeAsync(Guid id, int quantity, bool isIncrease)
        {
            var item = await _repository.GetByIdAsync(id);
            var aggregate = new ProductAggregate(item, _eventCollector);

            if (isIncrease)
                aggregate.IncreaseStock(quantity);
            else
                aggregate.DecreaseStock(quantity);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task ProductPriceChangeAsync(ProductPriceChangeDTO dto)
        {
            var item = await _repository.GetByIdAsync(dto.Id);
            var aggregate = new ProductAggregate(item, _eventCollector);
            aggregate.ChangePrice(dto.Price);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            var aggregate = new ProductAggregate(item, _eventCollector);
            aggregate.DeleteProduct();

            await _repository.DeleteAsync(item);
        }
    }
}