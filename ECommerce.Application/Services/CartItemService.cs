using AutoMapper;
using ECommerce.Application.DTOs.CartItem;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Services;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly IRepository<CartItem> _repository;
        private readonly IServiceHelper<CartItem> _serviceHelper;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public CartItemService(IRepository<CartItem> repository, IServiceHelper<CartItem> serviceHelper, IProductService productService, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _serviceHelper = serviceHelper;
            _productService = productService;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<CartItemDTO>> GetAllCartItemsAsync(RequestParams? requestParams = null)
        {
            var items = await _serviceHelper.GetAllAsync(requestParams);

            return _mapper.Map<List<CartItemDTO>>(items);
        }

        public async Task<List<CartItemDTO>> GetAllCartItemsByUserAsync(Guid userId, RequestParams? requestParams = null)
        {
            var query = _repository.GetQuery()
                .Where(x => x.UserId == userId);

            var items = await _serviceHelper.GetAllAsync(requestParams, query);

            return _mapper.Map<List<CartItemDTO>>(items);
        }

        public async Task<CartItemDTO> GetCartItemByIdAsync(Guid id)
        {
            var item = await _serviceHelper.GetByIdAsync(id);

            return _mapper.Map<CartItemDTO>(item);
        }

        public async Task<CartItemDTO> GetSpecificCartItemByUserAsync(Guid id, Guid userId)
        {
            var query = _repository.GetQuery()
                .Where(x => x.UserId == userId);

            var item = await _serviceHelper.GetByIdAsync(id, query);

            return _mapper.Map<CartItemDTO>(item);
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
            var cartItem = await GetSpecificCartItemByUserAsync(dto.Id, dto.UserId);
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

        public async Task DeleteCartItemByUserAsync(Guid id, Guid userId)
        {
            var query = _repository.GetQuery()
                .Where(x => x.Id == id && x.UserId == userId);

            var item = await _repository.GetByIdAsync(id, query);
            var aggregate = new CartItemAggregate(item, _eventCollector);
            aggregate.DeleteCartItem();

            await _repository.DeleteAsync(item);
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
            var items = await GetAllCartItemsByUserAsync(userId);

            foreach (var item in items)
                await DeleteCartItemByUserAsync(item.Id, userId);
        }
    }
}