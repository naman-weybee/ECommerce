using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly IRepository<CartItemAggregate, CartItem> _repository;
        private readonly IMapper _mapper;

        public CartItemService(IRepository<CartItemAggregate, CartItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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

        public async Task CreateCartItemAsync(CartItemCreateDTO dto)
        {
            var item = _mapper.Map<CartItem>(dto);
            var aggregate = new CartItemAggregate(item);
            aggregate.CreateCartItem(item);

            await _repository.InsertAsync(aggregate);
        }

        public async Task UpdateCartItemAsync(CartItemUpdateDTO dto)
        {
            var item = _mapper.Map<CartItem>(dto);
            var aggregate = new CartItemAggregate(item);
            aggregate.UpdateCartItem(item);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task DeleteCartItemAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            var aggregate = new CartItemAggregate(item);
            aggregate.DeleteCartItem();

            await _repository.DeleteAsync(item);
        }
    }
}