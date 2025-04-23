using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Services;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly IRepository<CartItem> _repository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public CartItemService(IRepository<CartItem> repository, IProductService productService, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _productService = productService;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<CartItemDTO>> GetAllCartItemsAsync(RequestParams requestParams, Guid userId = default)
        {
            var query = _repository.GetQuery();

            if (userId != default)
                query = query.Where(x => x.UserId == userId);

            var items = await _repository.GetAllAsync(requestParams, query);

            return _mapper.Map<List<CartItemDTO>>(items);
        }

        public async Task<CartItemDTO> GetCartItemByIdAsync(Guid id, Guid userId)
        {
            var query = _repository.GetQuery()
                .Where(x => x.UserId == userId);

            var item = await _repository.GetByIdAsync(id, query);

            return _mapper.Map<CartItemDTO>(item);
        }

        public async Task<List<CartItemDTO>> GetCartItemsByUserIdAsync(Guid userId)
        {
            var items = await _repository.GetQuery()
                .Where(x => x.UserId == userId).ToListAsync();

            return _mapper.Map<List<CartItemDTO>>(items);
        }

        public async Task CreateCartItemAsync(CartItemCreateDTO dto)
        {
            var product = await _productService.GetProductByIdAsync(dto.ProductId);

            var item = _mapper.Map<CartItem>(dto);
            var aggregate = new CartItemAggregate(item, _eventCollector);
            aggregate.CreateCartItem(item, product.Price);

            await _repository.InsertAsync(aggregate.Entity);
        }

        public async Task UpdateCartItemAsync(CartItemUpdateDTO dto)
        {
            var product = await _productService.GetProductByIdAsync(dto.ProductId);

            var item = _mapper.Map<CartItem>(dto);
            var aggregate = new CartItemAggregate(item, _eventCollector);
            aggregate.UpdateCartItem(item, product.Price);

            await _repository.UpdateAsync(aggregate.Entity);
        }

        public async Task UpdateQuantityAsync(CartItemQuantityUpdateDTO dto)
        {
            var cartItem = await GetCartItemByIdAsync(dto.Id, dto.UserId);
            var product = await _productService.GetProductByIdAsync(cartItem.ProductId);

            var item = _mapper.Map<CartItem>(cartItem);
            var aggregate = new CartItemAggregate(item, _eventCollector);
            aggregate.UpdateQuantity(dto.Quantity, product.Price);

            await _repository.UpdateAsync(aggregate.Entity);
        }

        public async Task UpdateUnitPriceAsync(CartItemUnitPriceUpdateDTO dto)
        {
            var item = _mapper.Map<CartItem>(dto);
            var aggregate = new CartItemAggregate(item, _eventCollector);
            aggregate.UpdateUnitPrice(dto.UnitPrice);

            await _repository.UpdateAsync(aggregate.Entity);
        }

        public async Task DeleteCartItemAsync(Guid id, Guid userId)
        {
            var query = _repository.GetQuery()
                .Where(x => x.Id == id && x.UserId == userId);

            var item = await _repository.GetByIdAsync(id, query);
            var aggregate = new CartItemAggregate(item, _eventCollector);
            aggregate.DeleteCartItem();

            await _repository.DeleteAsync(item);
        }

        public async Task ClearCartItemsAsync(Guid userId)
        {
            var items = await _repository.GetQuery()
                .Where(x => x.UserId == userId).ToListAsync();

            foreach (var item in items)
            {
                await DeleteCartItemAsync(item.Id, userId);
            }
        }
    }
}