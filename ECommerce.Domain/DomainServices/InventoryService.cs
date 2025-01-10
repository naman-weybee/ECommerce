using ECommerce.Domain.Entities;

namespace ECommerce.Domain.DomainServices
{
    public class InventoryService : IInventoryService
    {
        private readonly IIProductDomainService _productDomainService;

        public InventoryService(IIProductDomainService productDomainService)
        {
            _productDomainService = productDomainService;
        }

        public async Task ValidateCartItemsAsync(List<CartItem> cartItems)
        {
            foreach (var cartItem in cartItems)
            {
                var productStock = await _productDomainService.GetProductStockByIdAsync(cartItem.ProductId);

                if (productStock == 0 || productStock < cartItem.Quantity)
                    throw new InvalidOperationException($"Product {cartItem.ProductId} is out of stock or unavailable.");
            }
        }

        public async Task StockChange(Guid productId, int quantity, bool isIncrease)
        {
            await _productDomainService.ProductStockChangeAsync(productId, quantity, isIncrease);
        }
    }
}