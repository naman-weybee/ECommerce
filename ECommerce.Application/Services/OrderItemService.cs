using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Services;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IRepository<OrderItemAggregate, OrderItem> _repository;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public OrderItemService(IRepository<OrderItemAggregate, OrderItem> repository, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<OrderItemDTO>> GetAllOrderItemsAsync(RequestParams requestParams)
        {
            var query = _repository.GetDbSet();
            var items = await _repository.GetAllAsync(requestParams, query);

            return _mapper.Map<List<OrderItemDTO>>(items);
        }

        public async Task<OrderItemDTO> GetOrderItemByIdAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);

            return _mapper.Map<OrderItemDTO>(item);
        }

        public async Task<List<OrderItemDTO>> GetOrderItemByOrderIdAsync(Guid orderId)
        {
            var items = await _repository.GetAllByPropertyAsync("OrderId", orderId);

            return _mapper.Map<List<OrderItemDTO>>(items);
        }

        public async Task CreateOrderItemAsync(OrderItemCreateDTO dto)
        {
            var item = _mapper.Map<OrderItem>(dto);
            var aggregate = new OrderItemAggregate(item, _eventCollector);
            aggregate.CreateOrderItem(item);

            await _repository.InsertAsync(aggregate);
        }

        public async Task UpdateOrderItemAsync(OrderItemUpdateDTO dto)
        {
            var item = _mapper.Map<OrderItem>(dto);
            var aggregate = new OrderItemAggregate(item, _eventCollector);
            aggregate.UpdateOrderItem(item);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task UpdateQuantityAsync(OrderItemQuantityUpdateDTO dto)
        {
            var item = _mapper.Map<OrderItem>(dto);
            var aggregate = new OrderItemAggregate(item, _eventCollector);
            aggregate.UpdateQuantity(dto.Quantity);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task UpdateUnitPriceAsync(OrderItemUnitPriceUpdateDTO dto)
        {
            var item = _mapper.Map<OrderItem>(dto);
            var aggregate = new OrderItemAggregate(item, _eventCollector);
            aggregate.UpdateUnitPrice(dto.UnitPrice);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task DeleteOrderItemAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            var aggregate = new OrderItemAggregate(item, _eventCollector);
            aggregate.DeleteOrderItem();

            await _repository.DeleteAsync(item);
        }
    }
}