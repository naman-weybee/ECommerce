namespace ECommerce.Domain.DomainServices.Interfaces
{
    public interface IInventoryService
    {
        Task ValidatProductStockAsync(Guid productId, int quantity);

        Task ProductStockChangeAsync(Guid productId, int quantity, bool isIncrease);
    }
}