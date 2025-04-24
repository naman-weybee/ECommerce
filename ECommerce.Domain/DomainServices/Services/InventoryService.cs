using ECommerce.Domain.DomainServices.Interfaces;

namespace ECommerce.Domain.DomainServices.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IProductDomainService _productDomainService;

        public InventoryService(IProductDomainService productDomainService)
        {
            _productDomainService = productDomainService;
        }

        public async Task ValidatProductStockAsync(Guid productId, int quantity)
        {
            var productStock = await _productDomainService.GetProductStockByIdAsync(productId);

            if (productStock == 0 || productStock < quantity)
                throw new InvalidOperationException($"Product {productId} is out of stock.");
        }

        public async Task ProductStockChangeAsync(Guid productId, int quantity, bool isIncrease)
        {
            await _productDomainService.ProductStockChangeAsync(productId, quantity, isIncrease);
        }
    }
}