﻿using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using MediatR;

namespace ECommerce.Domain.Aggregates
{
    public class OrderAggregate : AggregateRoot<Order>
    {
        private readonly IMediator _mediator;

        public Order Order { get; set; }

        public OrderAggregate(Order entity, IMediator mediator)
             : base(entity, mediator)
        {
            Order = entity;
            Order.OrderItems ??= new List<OrderItem>();
            _mediator = mediator;
        }

        public async Task CreateOrder(Order order)
        {
            Order.CreateOrder(order.Id, order.UserId, order.AddressId, order.OrderStatus, order.TotalAmount, order.PaymentMethod, order.OrderItems);

            EventType = eEventType.OrderCreated;
            await RaiseDomainEvent();
        }

        public async Task UpdateOrder(Order order)
        {
            Order.UpdateOrder(order.Id, order.UserId, order.AddressId, order.OrderStatus, order.TotalAmount, order.PaymentMethod, order.OrderItems);

            EventType = eEventType.OrderUpdated;
            await RaiseDomainEvent();
        }

        public async Task AddOrderItem(OrderItem item)
        {
            ValidateOrderForModification();

            if (item.Quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(item.Quantity));

            if (item.UnitPrice.Amount <= 0)
                throw new ArgumentException("Unit price must be greater than zero.", nameof(item.UnitPrice.Amount));

            Order.AddOrderItem(item);

            EventType = eEventType.OrderItemAddedInOrder;
            await RaiseDomainEvent();
        }

        public async Task RemoveOrderItem(Guid orderItemId)
        {
            ValidateOrderForModification();

            var item = Order.OrderItems.FirstOrDefault(o => o.Id == orderItemId)
                ?? throw new InvalidOperationException("Order item not found.");

            Order.RemoveOrderItem(item);

            EventType = eEventType.OrderItemRemovedFromOrder;
            await RaiseDomainEvent();
        }

        public void UpdateOrderStatus(eOrderStatus newStatus)
        {
            if (!Order.OrderItems.Any())
                throw new InvalidOperationException("Cannot change order status of an order without items.");

            switch (newStatus)
            {
                case eOrderStatus.Placed:
                    OrderPlacedEvent();
                    break;

                case eOrderStatus.Shipped:
                    OrderShippedEvent();
                    break;

                case eOrderStatus.Delivered:
                    OrderDeliveredEvent();
                    break;

                case eOrderStatus.Canceled:
                    OrderCanceledEvent();
                    break;
            }
        }

        public async Task UpdatePaymentMethod(string newPaymentMethod)
        {
            ValidateOrderForModification();
            Order.UpdatePaymentMethod(newPaymentMethod);
            await RaiseDomainEvent();
        }

        public async Task UpdateShippingAddress(Guid addressId)
        {
            ValidateOrderForModification();
            Order.UpdateShippingAddress(addressId);
            await RaiseDomainEvent();
        }

        private void ValidateOrderForModification()
        {
            if (Order.OrderStatus == eOrderStatus.Canceled)
                throw new InvalidOperationException("Cannot modify a canceled order.");
        }

        private void OrderPlacedEvent()
        {
            if (Order.OrderStatus != eOrderStatus.Pending)
                throw new InvalidOperationException("Order can only be Placed if it is Pending.");

            Order.UpdateOrderStatus(eOrderStatus.Placed);
        }

        private void OrderShippedEvent()
        {
            if (Order.OrderStatus != eOrderStatus.Placed)
                throw new InvalidOperationException("Order can only be Shipped if it is Placed.");

            Order.UpdateOrderStatus(eOrderStatus.Shipped);
        }

        private void OrderDeliveredEvent()
        {
            if (Order.OrderStatus != eOrderStatus.Shipped)
                throw new InvalidOperationException("Order can only be Delivered if it is Shipped.");

            Order.UpdateOrderStatus(eOrderStatus.Delivered);
        }

        private void OrderCanceledEvent()
        {
            if (Order.OrderStatus == eOrderStatus.Delivered || Order.OrderStatus == eOrderStatus.Canceled)
                throw new InvalidOperationException("Canceled status is not allowed for Delivered or already Canceled orders.");

            Order.UpdateOrderStatus(eOrderStatus.Canceled);
        }

        public async Task DeleteOrder()
        {
            EventType = eEventType.OrderDeleted;
            await RaiseDomainEvent();
        }

        private async Task RaiseDomainEvent()
        {
            var domainEvent = new OrderEvent(Order.Id, Order.UserId, Order.TotalAmount.Amount, EventType);
            await RaiseDomainEvent(domainEvent);
        }

        public new void ClearDomainEvents()
        {
            base.ClearDomainEvents();
        }
    }
}