using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.DomainInterfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.ValueObjects;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<OrderAggregate, Order> _repository;
        private readonly ICartItemService _cartItemService;
        private readonly IOrderItemService _orderItemService;
        private readonly IUserService _userService;
        private readonly IInventoryService _inventoryService;
        private readonly IMapper _mapper;

        public OrderService(IRepository<OrderAggregate, Order> repository, ICartItemService cartItemService, IOrderItemService orderItemService, IUserService userService, IInventoryService inventoryService, IMapper mapper)
        {
            _repository = repository;
            _cartItemService = cartItemService;
            _orderItemService = orderItemService;
            _userService = userService;
            _inventoryService = inventoryService;
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

        public async Task CreateOrderAsync(OrderCreateFromCartDTO dto)
        {
            //Get User
            var user = await _userService.GetUserByIdAsync(dto.UserId)
                ?? throw new InvalidOperationException("User not found.");

            //Get Cart Items Based on UserId
            var cartItems = await _cartItemService.GetCartItemsByUserIdAsync(user.Id);
            if (!cartItems.Any() || cartItems.Count == 0)
                throw new InvalidOperationException("Cart is empty!");

            //Check Product Stock Availability
            var cartItemEntities = _mapper.Map<List<CartItem>>(cartItems);
            await _inventoryService.ValidateCartItemsAsync(cartItemEntities);

            //Calculate Total Amount
            var totalAmount = cartItems.Sum(c => c.Quantity * c.UnitPrice.Amount);

            //Create Order
            var orderId = Guid.NewGuid();

            var orderDto = new OrderCreateDTO
            {
                Id = orderId,
                UserId = user.Id,
                TotalAmount = new Money(totalAmount),
                PaymentMethod = dto.PaymentMethod,
                ShippingAddressId = user.AddressId
            };

            var order = _mapper.Map<Order>(orderDto);
            var aggregate = new OrderAggregate(order);
            aggregate.CreateOrder(order);

            await _repository.InsertAsync(aggregate);

            //Create Order Items
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

            // Adjust Prodct Stock - Domain Service
            foreach (var cartItem in cartItems)
                await _inventoryService.StockChange(cartItem.ProductId, cartItem.Quantity, false);

            //Clear Cart Item
            await _cartItemService.ClearCartItemsAsync(user.Id);
        }

        public async Task UpdateOrderAsync(OrderUpdateDTO dto)
        {
            var item = _mapper.Map<Order>(dto);
            var aggregate = _mapper.Map<OrderAggregate>(item);
            aggregate.UpdateOrder(aggregate.Order);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task UpdateOrderStatusAsync(OrderUpdateStatusDTO dto)
        {
            var query = _repository.GetDbSet();
            query = query.Include(c => c.OrderItems);

            var order = await _repository.GetByIdAsync(dto.Id, query);
            var aggregate = _mapper.Map<OrderAggregate>(order);
            aggregate.UpdateOrderStatus(dto.OrderStatus);

            if (dto.OrderStatus == eOrderStatus.Canceled)
            {
                var orderItems = await _orderItemService.GetOrderItemByOrderIdAsync(dto.Id);
                if (!orderItems.Any() || orderItems.Count == 0)
                    throw new InvalidOperationException("Order is empty!");

                // Adjust Prodct Stock - Domain Service
                foreach (var orderItem in orderItems)
                    await _inventoryService.StockChange(orderItem.ProductId, orderItem.Quantity, true);
            }

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