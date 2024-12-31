using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<OrderAggregate, Order> _repository;
        private readonly IMapper _mapper;

        public OrderService(IRepository<OrderAggregate, Order> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<OrderDTO>> GetAllOrdersAsync(RequestParams requestParams)
        {
            var query = _repository.GetDbSet();
            query = query.Include(c => c.OrderItems);

            var items = await _repository.GetAllAsync(requestParams, query);

            return _mapper.Map<List<OrderDTO>>(items);
        }

        public async Task<OrderDTO> GetOrderByIdAsync(Guid id)
        {
            var query = _repository.GetDbSet();
            query = query.Include(c => c.OrderItems);

            var item = await _repository.GetByIdAsync(id, query);

            return _mapper.Map<OrderDTO>(item);
        }

        public async Task CreateOrderAsync(OrderCreateDTO dto)
        {
            var order = _mapper.Map<Order>(dto);
            var aggregate = new OrderAggregate(order);
            aggregate.CreateOrder(order);

            await _repository.InsertAsync(aggregate);
        }

        public async Task UpdateOrderAsync(OrderUpdateDTO dto)
        {
            var order = _mapper.Map<Order>(dto);
            var aggregate = _mapper.Map<OrderAggregate>(order);
            aggregate.UpdateOrder(aggregate.Order);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task DeleteOrderAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            var aggregate = _mapper.Map<OrderAggregate>(item);
            aggregate.DeleteOrder();

            await _repository.DeleteAsync(item);
        }
    }
}