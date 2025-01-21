using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.DomainServices;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.ValueObjects;
using ECommerce.Infrastructure.Services;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<OrderAggregate, Order> _repository;
        private readonly IUserService _userService;
        private readonly ICartItemService _cartItemService;
        private readonly IOrderItemService _orderItemService;
        private readonly IInventoryService _inventoryService;
        private readonly ITransactionManagerService _transactionManagerService;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public OrderService(IRepository<OrderAggregate, Order> repository, IUserService userService, ICartItemService cartItemService, IOrderItemService orderItemService, IInventoryService inventoryService, ITransactionManagerService transactionManagerService, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _userService = userService;
            _cartItemService = cartItemService;
            _orderItemService = orderItemService;
            _inventoryService = inventoryService;
            _transactionManagerService = transactionManagerService;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<OrderDTO>> GetAllOrdersAsync(RequestParams requestParams)
        {
            var query = _repository.GetDbSet();
            query = query.Include(c => c.OrderItems);

            var items = await _repository.GetAllAsync(requestParams, query);

            return _mapper.Map<List<OrderDTO>>(items);
        }

        public async Task<List<OrderDTO>> GetAllRecentOrdersAsync(RequestParams requestParams, Guid userId)
        {
            var query = _repository.GetDbSet();
            query = query.Include(c => c.OrderItems).Where(u => u.UserId == userId);

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

        public async Task CreateOrderAsync(OrderCreateFromCartDTO dto)
        {
            // Begin Transaction
            await _transactionManagerService.BeginTransactionAsync();

            try
            {
                // Get User
                var user = await _userService.GetUserByIdAsync(dto.UserId);

                // Get Cart Items
                var cartItems = await GetUserCartItems(user.Id);

                // Check Product Stock Availability
                await CheckProductStockAvailability(cartItems);

                // Calculate Total Amount
                var totalAmount = cartItems.Sum(c => c.Quantity * c.UnitPrice.Amount);

                // Create Order
                var orderId = Guid.NewGuid();
                await CreateOrderFromCartItems(orderId, user.Id, dto.PaymentMethod, totalAmount, user.AddressId);

                // Create Order Items
                await CreateOrderItemsFromCartItems(orderId, cartItems);

                // Update Prodct Stock - Domain Service
                await UpdateProductStock(cartItems, false);

                //Clear Cart
                await ClearCart(user.Id);

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

        public async Task UpdateOrderAsync(OrderUpdateDTO dto)
        {
            var item = _mapper.Map<Order>(dto);
            var aggregate = new OrderAggregate(item, _eventCollector);
            aggregate.UpdateOrder(aggregate.Order);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task UpdateOrderStatusAsync(OrderUpdateStatusDTO dto)
        {
            var query = _repository.GetDbSet();
            query = query.Include(c => c.OrderItems);

            var order = await _repository.GetByIdAsync(dto.Id, query);
            var aggregate = new OrderAggregate(order, _eventCollector);
            aggregate.UpdateOrderStatus(dto.OrderStatus);

            // For Order Cancelation
            if (dto.OrderStatus == eOrderStatus.Canceled)
            {
                // Get Order Items
                var orderItems = await GetOrderItems(dto);

                // Update Prodct Stock - Domain Service
                await UpdateProductStock(orderItems, true);
            }

            await _repository.UpdateAsync(aggregate);
        }

        public async Task DeleteOrderAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            var aggregate = new OrderAggregate(item, _eventCollector);
            aggregate.DeleteOrder();

            await _repository.DeleteAsync(item);
        }

        private async Task<List<CartItemDTO>> GetUserCartItems(Guid userId)
        {
            var cartItems = await _cartItemService.GetCartItemsByUserIdAsync(userId);
            if (!cartItems.Any() || cartItems.Count == 0)
                throw new InvalidOperationException("Cart is empty!");

            return cartItems;
        }

        private async Task CheckProductStockAvailability(List<CartItemDTO> cartItems)
        {
            var cartItemEntities = _mapper.Map<List<CartItem>>(cartItems);
            await _inventoryService.ValidateCartItemsAsync(cartItemEntities);
        }

        private async Task CreateOrderFromCartItems(Guid orderId, Guid userId, string paymentMethod, decimal totalAmount, Guid addressId)
        {
            var orderDto = new OrderCreateDTO
            {
                Id = orderId,
                UserId = userId,
                TotalAmount = new Money(totalAmount),
                PaymentMethod = paymentMethod,
                AddressId = addressId
            };

            var order = _mapper.Map<Order>(orderDto);
            var aggregate = new OrderAggregate(order, _eventCollector);
            aggregate.CreateOrder(order);

            await _repository.InsertAsync(aggregate);
        }

        private async Task CreateOrderItemsFromCartItems(Guid orderId, List<CartItemDTO> cartItems)
        {
            foreach (var cartItem in cartItems)
            {
                var orderItem = new OrderItemCreateDTO
                {
                    OrderId = orderId,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.UnitPrice
                };

                await _orderItemService.CreateOrderItemAsync(orderItem);
            }
        }

        private async Task<List<OrderItemDTO>> GetOrderItems(OrderUpdateStatusDTO dto)
        {
            var orderItems = await _orderItemService.GetOrderItemByOrderIdAsync(dto.Id);
            if (!orderItems.Any() || orderItems.Count == 0)
                throw new InvalidOperationException("Order is empty!");
            return orderItems;
        }

        private async Task UpdateProductStock(List<CartItemDTO> cartItems, bool isIncrease)
        {
            foreach (var cartItem in cartItems)
                await _inventoryService.StockChange(cartItem.ProductId, cartItem.Quantity, isIncrease);
        }

        private async Task UpdateProductStock(List<OrderItemDTO> orderItems, bool isIncrease)
        {
            foreach (var orderItem in orderItems)
                await _inventoryService.StockChange(orderItem.ProductId, orderItem.Quantity, isIncrease);
        }

        private async Task ClearCart(Guid userId)
        {
            await _cartItemService.ClearCartItemsAsync(userId);
        }
    }
}