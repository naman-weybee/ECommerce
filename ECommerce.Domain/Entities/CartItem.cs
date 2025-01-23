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

        public void CreateCartItem(Guid userId, Guid productId, int quantity, Money productPrice)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = new Money(quantity * productPrice.Amount);
        }

        public void UpdateCartItem(Guid id, Guid userId, Guid productId, int quantity, Money productPrice)
        {
            Id = id;
            UserId = userId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = new Money(quantity * productPrice.Amount);

            StatusUpdated();
        }

        public void UpdateQuantity(int newQuantity, Money productPrice)
        {
            Quantity = newQuantity;
            UnitPrice = new Money(newQuantity * productPrice.Amount);

            StatusUpdated();
        }

        public void UpdateUnitPrice(Money newUnitPrice)
        {
            UnitPrice = newUnitPrice;
            StatusUpdated();
        }
    }
}