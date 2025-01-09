using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.DomainInterfaces;
using ECommerce.Domain.Entities;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;
using MediatR;

namespace ECommerce.Application.Services
{
    public class ProductService : IProductService, IIProductDomainService
    {
        private readonly IRepository<ProductAggregate, Product> _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductService(IRepository<ProductAggregate, Product> repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
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
            var aggregate = new ProductAggregate(item, _mediator);
            await aggregate.CreateProduct(item);

            await _repository.InsertAsync(aggregate);
        }

        public async Task UpdateProductAsync(ProductUpdateDTO dto)
        {
            var item = _mapper.Map<Product>(dto);
            var aggregate = new ProductAggregate(item, _mediator);
            await aggregate.UpdateProduct(aggregate.Product);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task ProductStockChangeAsync(Guid id, int quantity, bool isIncrease)
        {
            var item = await _repository.GetByIdAsync(id);
            var aggregate = new ProductAggregate(item, _mediator);

            if (isIncrease)
                await aggregate.IncreaseStock(quantity);
            else
                await aggregate.DecreaseStock(quantity);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task ProductPriceChangeAsync(ProductPriceChangeDTO dto)
        {
            var item = await _repository.GetByIdAsync(dto.Id);
            var aggregate = new ProductAggregate(item, _mediator);
            await aggregate.ChangePrice(dto.Price);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            var aggregate = new ProductAggregate(item, _mediator);
            await aggregate.DeleteProduct();

            await _repository.DeleteAsync(item);
        }
    }
}