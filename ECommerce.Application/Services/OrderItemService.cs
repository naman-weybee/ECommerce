using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.DomainServices.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.ValueObjects;
using ECommerce.Infrastructure.Services;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IRepository<OrderItemAggregate, OrderItem> _repository;
        private readonly IRepository<OrderAggregate, Order> _orderRepository;
        private readonly IProductService _productService;
        private readonly IInventoryService _inventoryService;
        private readonly ITransactionManagerService _transactionManagerService;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public OrderItemService(IRepository<OrderItemAggregate, OrderItem> repository, IRepository<OrderAggregate, Order> orderRepository, IProductService productService, IInventoryService inventoryService, ITransactionManagerService transactionManagerService, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _orderRepository = orderRepository;
            _productService = productService;
            _inventoryService = inventoryService;
            _transactionManagerService = transactionManagerService;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<OrderItemDTO>> GetAllOrderItemsAsync(RequestParams requestParams)
        {
            var items = await _repository.GetAllAsync(requestParams);

            return _mapper.Map<List<OrderItemDTO>>(items);
        }

        public async Task<OrderItemDTO> GetOrderItemByIdAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);

            return _mapper.Map<OrderItemDTO>(item);
        }

        public async Task<List<OrderItemDTO>> GetOrderItemsByOrderIdAsync(Guid orderId)
        {
            var items = await _repository.GetDbSet()
                .Where(x => x.OrderId == orderId).ToListAsync();

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
            var orderItem = await GetOrderItemByIdAsync(dto.Id);
            var product = await _productService.GetProductByIdAsync(orderItem.ProductId);

            var order = await _orderRepository.GetDbSet()
                .Include(x => x.OrderItems).SingleOrDefaultAsync(x => x.Id == orderItem.OrderId && x.UserId == dto.UserId);

            dto.UnitPrice = new Money(dto.Quantity * product.Price.Amount);

            var item = _mapper.Map<OrderItem>(dto);
            var aggregate = new OrderItemAggregate(item, _eventCollector);
            aggregate.UpdateOrderItem(item);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task UpdateQuantityAsync(OrderItemQuantityUpdateDTO dto)
        {
            // Begin Transaction
            await _transactionManagerService.BeginTransactionAsync();

            try
            {
                var orderItem = await GetOrderItemByIdAsync(dto.Id);

                // Check Product Stock Availability
                await ValidateProductStockAsync(orderItem.ProductId, orderItem.Quantity, dto.Quantity);

                var item = _mapper.Map<OrderItem>(orderItem);
                var product = await _productService.GetProductByIdAsync(orderItem.ProductId);
                var order = await _orderRepository.GetDbSet()
                    .Include(x => x.OrderItems).SingleOrDefaultAsync(x => x.Id == orderItem.OrderId && x.UserId == dto.UserId);

                var aggregate = new OrderItemAggregate(item, _eventCollector);
                aggregate.UpdateQuantity(dto.Quantity, product.Price);

                await _repository.UpdateAsync(aggregate);

                // Update Product Stock
                await UpdateOrderItemProductStockAsync(product.Id, orderItem.Quantity, dto.Quantity);

                // Update Total Amount of Order
                await UpdateOrderTotalAmountAsync(orderItem.OrderId, dto.UserId);

                // Commit transaction
                await _transactionManagerService.CommitTransactionAsync();
            }
            catch (Exception)
            {
                // Rollback transaction on error
                await _transactionManagerService.RollbackTransactionAsync();
                throw;
            }
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

        private async Task UpdateOrderTotalAmountAsync(Guid orderId, Guid userId)
        {
            var item = await _orderRepository.GetDbSet()
                .Include(x => x.OrderItems).SingleOrDefaultAsync(x => x.Id == orderId && x.UserId == userId);

            var aggregate = new OrderAggregate(item!, _eventCollector);
            aggregate.UpdateTotalAmount();

            await _orderRepository.UpdateAsync(aggregate);
        }

        private async Task ValidateProductStockAsync(Guid productId, int oldQuantity, int newQuantity)
        {
            if (oldQuantity == newQuantity)
                throw new InvalidOperationException("You've entered exact same quantity as it was, please add diff quantity to update.");

            if (oldQuantity < newQuantity)
                await _inventoryService.ValidatProductStockAsync(productId, newQuantity - oldQuantity);
        }

        private async Task UpdateOrderItemProductStockAsync(Guid productId, int oldQuantity, int newQuantity)
        {
            if (oldQuantity > newQuantity)
                await _inventoryService.ProductStockChangeAsync(productId, oldQuantity - newQuantity, true);

            if (oldQuantity < newQuantity)
                await _inventoryService.ProductStockChangeAsync(productId, newQuantity - oldQuantity, false);
        }
    }
}