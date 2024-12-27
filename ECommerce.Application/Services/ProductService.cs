using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<ProductAggregate, Product> _repository;
        private readonly IMapper _mapper;

        public ProductService(IRepository<ProductAggregate, Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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

        public async Task CreateProductAsync(ProductCreateDTO dto)
        {
            var product = _mapper.Map<Product>(dto);
            var aggregate = new ProductAggregate(product);
            aggregate.CreateProduct(product);

            await _repository.InsertAsync(aggregate);
        }

        public async Task UpdateProductAsync(ProductUpdateDTO dto)
        {
            var aggregate = _mapper.Map<ProductAggregate>(dto);
            aggregate.UpdateProduct(aggregate.Product);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            var aggregate = _mapper.Map<ProductAggregate>(item);
            aggregate.DeleteProduct();

            await _repository.DeleteAsync(item);
        }
    }
}