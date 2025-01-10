using ECommerce.Domain.Entities;

namespace ECommerce.Domain.DomainServices
{
    public interface IInventoryService
    {
        Task ValidateCartItemsAsync(List<CartItem> cartItems);

        Task StockChange(Guid productId, int quantity, bool isIncrease);
    }
}