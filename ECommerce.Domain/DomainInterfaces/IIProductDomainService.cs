namespace ECommerce.Domain.DomainInterfaces
{
    public interface IIProductDomainService
    {
        Task<int> GetProductStockByIdAsync(Guid id);

        Task ProductStockChangeAsync(Guid productId, int quantity, bool isIncrease);
    }
}