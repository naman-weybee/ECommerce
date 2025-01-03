using ECommerce.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Entities
{
    public class CartItem : Base
    {
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public Money UnitPrice { get; set; }

        public virtual User User { get; set; }

        public virtual Product Product { get; set; }

        public CartItem()
        {
        }

        public CartItem(Guid userId, Guid productId, int quantity, Money unitPrice)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public void CreateCartItem(Guid userId, Guid productId, int quantity, Money unitPrice)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public void UpdateCartItem(Guid id, Guid userId, Guid productId, int quantity, Money unitPrice)
        {
            Id = id;
            UserId = userId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;

            StatusUpdated();
        }

        public void UpdateQuantity(int newQuantity)
        {
            Quantity = newQuantity;
            StatusUpdated();
        }

        public void UpdateUnitPrice(Money newUnitPrice)
        {
            UnitPrice = newUnitPrice;
            StatusUpdated();
        }
    }
}