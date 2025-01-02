using ECommerce.Application.DTOs;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllProductsAsync(RequestParams dto);

        Task<ProductDTO> GetProductByIdAsync(Guid id);

        Task CreateProductAsync(ProductCreateDTO dto);

        Task UpdateProductAsync(ProductUpdateDTO dto);

        Task ProductStockChangeAsync(ProductStockChangeDTO dto, bool isIncrease);

        Task ProductPriceChangeAsync(ProductPriceChangeDTO dto);

        Task DeleteProductAsync(Guid id);
    }
}