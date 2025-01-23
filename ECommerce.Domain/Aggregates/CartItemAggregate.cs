using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using ECommerce.Domain.ValueObjects;
using ECommerce.Infrastructure.Services;

namespace ECommerce.Domain.Aggregates
{
    public class CartItemAggregate : AggregateRoot<CartItem>
    {
        private readonly IDomainEventCollector _eventCollector;

        public CartItem CartItem { get; set; }

        public CartItemAggregate(CartItem entity, IDomainEventCollector eventCollector)
            : base(entity, eventCollector)
        {
            CartItem = entity;
            _eventCollector = eventCollector;
        }

        public void CreateCartItem(CartItem cartItem, Money productPrice)
        {
            CartItem.CreateCartItem(cartItem.UserId, cartItem.ProductId, cartItem.Quantity, productPrice);

            EventType = eEventType.CartItemCreated;
            RaiseDomainEvent();
        }

        public void UpdateCartItem(CartItem cartItem, Money productPrice)
        {
            CartItem.UpdateCartItem(cartItem.Id, cartItem.UserId, cartItem.ProductId, cartItem.Quantity, productPrice);

            EventType = eEventType.CartItemUpdated;
            RaiseDomainEvent();
        }

        public void UpdateQuantity(int newQuantity, Money productPrice)
        {
            if (newQuantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(newQuantity));

            CartItem.UpdateQuantity(newQuantity, productPrice);

            EventType = eEventType.OrderItemQuantityUpdated;
            RaiseDomainEvent();
        }

        public void UpdateUnitPrice(Money newUnitPrice)
        {
            if (newUnitPrice.Amount <= 0)
                throw new ArgumentException("Unit price must be greater than zero.", nameof(newUnitPrice));

            CartItem.UpdateUnitPrice(newUnitPrice);

            EventType = eEventType.OrderItemUnitPriceUpdated;
            RaiseDomainEvent();
        }

        public void DeleteCartItem()
        {
            EventType = eEventType.CartItemDeleted;
            RaiseDomainEvent();
        }

        private void RaiseDomainEvent()
        {
            var domainEvent = new CartItemEvent(CartItem.Id, CartItem.UserId, CartItem.ProductId, EventType);
            RaiseDomainEvent(domainEvent);
        }
    }
}