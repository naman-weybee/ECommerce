using ECommerce.Application.DTOs.Product;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllProductsAsync(RequestParams? requestParams = null);

        Task<ProductDTO> GetProductByIdAsync(Guid id);

        Task UpsertProductAsync(ProductUpsertDTO dto);

        Task ProductStockChangeAsync(Guid id, int quantity, bool isIncrease);

        Task ProductPriceChangeAsync(ProductPriceChangeDTO dto);

        Task DeleteProductAsync(Guid id);
    }
}