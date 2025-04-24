namespace ECommerce.Domain.DomainServices.Interfaces
{
    public interface IProductDomainService
    {
        Task<int> GetProductStockByIdAsync(Guid id);

        Task ProductStockChangeAsync(Guid productId, int quantity, bool isIncrease);
    }
}