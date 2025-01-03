using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IRepository<OrderItemAggregate, OrderItem> _repository;
        private readonly IMapper _mapper;

        public OrderItemService(IRepository<OrderItemAggregate, OrderItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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

        public async Task CreateOrderItemAsync(OrderItemCreateDTO dto)
        {
            var orderItem = _mapper.Map<OrderItem>(dto);
            var aggregate = new OrderItemAggregate(orderItem);
            aggregate.CreateOrderItem(orderItem);

            await _repository.InsertAsync(aggregate);
        }

        public async Task UpdateOrderItemAsync(OrderItemUpdateDTO dto)
        {
            var orderItem = _mapper.Map<OrderItem>(dto);
            var aggregate = new OrderItemAggregate(orderItem);
            aggregate.UpdateOrderItem(orderItem);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task DeleteOrderItemAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            var aggregate = new OrderItemAggregate(item);
            aggregate.DeleteOrderItem();

            await _repository.DeleteAsync(item);
        }
    }
}