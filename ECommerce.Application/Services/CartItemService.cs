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
        private readonly IRepository<CartItemAggregate, CartItem> _repository;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public CartItemService(IRepository<CartItemAggregate, CartItem> repository, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<CartItemDTO>> GetAllCartItemsAsync(RequestParams requestParams)
        {
            var items = await _repository.GetAllAsync(requestParams);

            return _mapper.Map<List<CartItemDTO>>(items);
        }

        public async Task<CartItemDTO> GetCartItemByIdAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);

            return _mapper.Map<CartItemDTO>(item);
        }

        public async Task<List<CartItemDTO>> GetCartItemsByUserIdAsync(Guid userId)
        {
            var query = _repository.GetDbSet();

            var items = await query.Where(x => x.UserId == userId).ToListAsync();

            return _mapper.Map<List<CartItemDTO>>(items);
        }

        public async Task CreateCartItemAsync(CartItemCreateDTO dto)
        {
            var item = _mapper.Map<CartItem>(dto);
            var aggregate = new CartItemAggregate(item, _eventCollector);
            aggregate.CreateCartItem(item);

            await _repository.InsertAsync(aggregate);
        }

        public async Task UpdateCartItemAsync(CartItemUpdateDTO dto)
        {
            var item = _mapper.Map<CartItem>(dto);
            var aggregate = new CartItemAggregate(item, _eventCollector);
            aggregate.UpdateCartItem(item);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task UpdateQuantityAsync(CartItemQuantityUpdateDTO dto)
        {
            var item = _mapper.Map<CartItem>(dto);
            var aggregate = new CartItemAggregate(item, _eventCollector);
            aggregate.UpdateQuantity(dto.Quantity);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task UpdateUnitPriceAsync(CartItemUnitPriceUpdateDTO dto)
        {
            var item = _mapper.Map<CartItem>(dto);
            var aggregate = new CartItemAggregate(item, _eventCollector);
            aggregate.UpdateUnitPrice(dto.UnitPrice);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task DeleteCartItemAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            var aggregate = new CartItemAggregate(item, _eventCollector);
            aggregate.DeleteCartItem();

            await _repository.DeleteAsync(item);
        }

        public async Task ClearCartItemsAsync(Guid userId)
        {
            var query = _repository.GetDbSet();

            var items = await query.Where(x => x.UserId == userId).ToListAsync();

            foreach (var item in items)
            {
                await DeleteCartItemAsync(item.Id);
            }
        }
    }
}