using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.DomainServices.Interfaces;
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
        private readonly IRepository<Order> _repository;
        private readonly IServiceHelper<Order> _serviceHelper;
        private readonly IRepository<Address> _addressRepository;
        private readonly IUserService _userService;
        private readonly ICartItemService _cartItemService;
        private readonly IOrderItemService _orderItemService;
        private readonly IInventoryService _inventoryService;
        private readonly IEmailTemplates _emailTemplates;
        private readonly ITransactionManagerService _transactionManagerService;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public OrderService(IRepository<Order> repository, IServiceHelper<Order> serviceHelper, IRepository<Address> addressRepository, IUserService userService, ICartItemService cartItemService, IOrderItemService orderItemService, IInventoryService inventoryService, IEmailTemplates emailTemplates, ITransactionManagerService transactionManagerService, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _serviceHelper = serviceHelper;
            _addressRepository = addressRepository;
            _userService = userService;
            _cartItemService = cartItemService;
            _orderItemService = orderItemService;
            _inventoryService = inventoryService;
            _emailTemplates = emailTemplates;
            _transactionManagerService = transactionManagerService;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<OrderDTO>> GetAllOrdersAsync(RequestParams? requestParams = null, bool useQuery = false)
        {
            IQueryable<Order> query = useQuery
                ? _repository.GetQuery().Include(c => c.OrderItems)!
                : null!;

            var items = await _serviceHelper.GetAllAsync(requestParams);

            return _mapper.Map<List<OrderDTO>>(items);
        }

        public async Task<List<OrderDTO>> GetAllOrdersByUserAsync(Guid userId, RequestParams? requestParams = null, bool useQuery = false)
        {
            IQueryable<Order> query = useQuery
                ? _repository.GetQuery().Where(x => x.UserId == userId).Include(c => c.OrderItems)!
                : null!;

            var items = await _serviceHelper.GetAllAsync(requestParams, query);

            return _mapper.Map<List<OrderDTO>>(items);
        }

        public async Task<List<OrderDTO>> GetAllRecentOrdersAsync(RequestParams? requestParams = null, bool useQuery = false)
        {
            IQueryable<Order> query = useQuery
                ? _repository.GetQuery().OrderByDescending(x => x.OrderPlacedDate).Include(c => c.OrderItems)!
                : null!;

            var items = await _serviceHelper.GetAllAsync(requestParams, query);

            return _mapper.Map<List<OrderDTO>>(items);
        }

        public async Task<List<OrderDTO>> GetAllRecentOrdersByUserAsync(Guid userId, RequestParams? requestParams = null, bool useQuery = false)
        {
            var query = _repository.GetQuery()
                .Where(x => x.UserId == userId);

            if (useQuery)
                query = query.Include(x => x.OrderItems);

            query = query.OrderByDescending(x => x.CreatedDate);

            var items = await _serviceHelper.GetAllAsync(requestParams, query);

            return _mapper.Map<List<OrderDTO>>(items);
        }

        public async Task<OrderDTO> GetOrderByIdAsync(Guid id, bool useQuery = false)
        {
            IQueryable<Order> query = useQuery
                ? _repository.GetQuery().Include(c => c.OrderItems)!
                : null!;

            var item = await _serviceHelper.GetByIdAsync(id, query);

            return _mapper.Map<OrderDTO>(item);
        }

        public async Task<OrderDTO> GetSpecificOrderByUserAsync(Guid id, Guid userId, bool useQuery = false)
        {
            IQueryable<Order> query = useQuery
                ? _repository.GetQuery().Where(x => x.UserId == userId).Include(c => c.OrderItems)!
                : null!;

            var item = await _serviceHelper.GetByIdAsync(id, query);

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
                await CreateOrderFromCartItems(user.Id, orderId, dto.BillingAddressId, dto.ShippingAddressId, dto.PaymentMethod, totalAmount);

                // Create Order Items
                await CreateOrderItemsFromCartItems(orderId, cartItems);

                // Update Prodct Stock - Domain Service
                await UpdateProductStock(cartItems, false);

                // Clear Cart
                await ClearCart(user.Id);

                // Commit transaction
                await _transactionManagerService.CommitTransactionAsync();

                // Send Email to User
                await _emailTemplates.SendOrderEmailAsync(user.Id, orderId, eEventType.OrderPlaced);
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

            await _repository.UpdateAsync(aggregate.Entity);
        }

        public async Task UpdateOrderStatusAsync(OrderUpdateStatusDTO dto)
        {
            var order = await GetSpecificOrderByUserAsync(dto.Id, dto.UserId);

            var item = _mapper.Map<Order>(order);
            var aggregate = new OrderAggregate(item, _eventCollector);
            aggregate.UpdateOrderStatus(dto.OrderStatus);

            // For Order Cancelation
            if (dto.OrderStatus == eOrderStatus.Canceled)
            {
                // Get Order Items
                var orderItems = await GetOrderItems(dto);

                // Update Prodct Stock - Domain Service
                await UpdateProductStock(orderItems, true);
            }

            await _repository.UpdateAsync(aggregate.Entity);

            // Send Email to User
            await _emailTemplates.SendOrderEmailAsync(item.UserId, item.Id, aggregate.EventType);
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
            var cartItems = await _cartItemService.GetAllCartItemsByUserAsync(userId);
            if (!cartItems.Any() || cartItems.Count == 0)
                throw new InvalidOperationException("Cart is empty!");

            return cartItems;
        }

        private async Task CheckProductStockAvailability(List<CartItemDTO> cartItems)
        {
            var items = _mapper.Map<List<CartItem>>(cartItems);

            foreach (var item in items)
                await _inventoryService.ValidatProductStockAsync(item.ProductId, item.Quantity);
        }

        private async Task CreateOrderFromCartItems(Guid userId, Guid orderId, Guid billingAddressId, Guid shippingAddressId, string paymentMethod, decimal totalAmount)
        {
            // Check for Address Existance
            await CheckForAddressExistance(userId, billingAddressId, eAddressType.Billing);
            await CheckForAddressExistance(userId, shippingAddressId, eAddressType.Shipping);

            var orderDto = new OrderCreateDTO
            {
                Id = orderId,
                UserId = userId,
                BillingAddressId = billingAddressId,
                ShippingAddressId = shippingAddressId,
                PaymentMethod = paymentMethod,
                TotalAmount = new Money(totalAmount)
            };

            var order = _mapper.Map<Order>(orderDto);
            var aggregate = new OrderAggregate(order, _eventCollector);
            aggregate.CreateOrder(order);

            await _repository.InsertAsync(aggregate.Entity);
        }

        private async Task CheckForAddressExistance(Guid userId, Guid addressId, eAddressType addressType)
        {
            var isAddressExist = await _addressRepository.GetQuery()
                .AnyAsync(x => x.Id == addressId && x.UserId == userId && x.AdderessType == addressType);

            if (!isAddressExist)
                throw new InvalidOperationException($"{addressType} Address with Id = {addressId} for User Id = {userId} is not exist.");
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
            var orderItems = await _orderItemService.GetOrderItemsByOrderAsync(dto.Id);
            if (!orderItems.Any() || orderItems.Count == 0)
                throw new InvalidOperationException("Order is empty!");

            return orderItems;
        }

        private async Task UpdateProductStock(List<CartItemDTO> cartItems, bool isIncrease)
        {
            foreach (var cartItem in cartItems)
                await _inventoryService.ProductStockChangeAsync(cartItem.ProductId, cartItem.Quantity, isIncrease);
        }

        private async Task UpdateProductStock(List<OrderItemDTO> orderItems, bool isIncrease)
        {
            foreach (var orderItem in orderItems)
                await _inventoryService.ProductStockChangeAsync(orderItem.ProductId, orderItem.Quantity, isIncrease);
        }

        private async Task ClearCart(Guid userId)
        {
            await _cartItemService.ClearCartItemsAsync(userId);
        }
    }
}