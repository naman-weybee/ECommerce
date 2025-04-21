using ECommerce.API.Helper.Interfaces;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Enums;
using ECommerce.Shared.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _service;
        private readonly IControllerHelper _controllerHelper;

        public ProductController(IHTTPHelper httpHelper, IProductService service, IControllerHelper controllerHelper)
            : base(httpHelper)
        {
            _service = service;
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] RequestParams requestParams)
        {
            var data = await _service.GetAllProductsAsync(requestParams);
            _controllerHelper.SetResponse(_response, data, requestParams);

            return StatusCode(200, _response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var data = await _service.GetProductByIdAsync(id);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Product, eUserPermission.HasCreateOrUpdatePermission);

            await _service.CreateProductAsync(dto);
            _controllerHelper.SetResponse(_response, "New Product Added Successfully.");

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Product, eUserPermission.HasCreateOrUpdatePermission);

            await _service.UpdateProductAsync(dto);
            _controllerHelper.SetResponse(_response, "Product Modified Successfully.");

            return StatusCode(200, _response);
        }

        [HttpPut("IncreaseStock")]
        public async Task<IActionResult> IncreaseStock([FromBody] ProductStockChangeDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Product, eUserPermission.HasCreateOrUpdatePermission);

            await _service.ProductStockChangeAsync(dto.Id, dto.Quantity, true);
            _controllerHelper.SetResponse(_response, "Product Stock Increased Successfully.");

            return StatusCode(200, _response);
        }

        [HttpPut("DecreaseStock")]
        public async Task<IActionResult> DecreaseStock([FromBody] ProductStockChangeDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Product, eUserPermission.HasCreateOrUpdatePermission);

            await _service.ProductStockChangeAsync(dto.Id, dto.Quantity, false);
            _controllerHelper.SetResponse(_response, "Product Stock Decreased Successfully.");

            return StatusCode(200, _response);
        }

        [HttpPut("PriceChange")]
        public async Task<IActionResult> PriceChange([FromBody] ProductPriceChangeDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Product, eUserPermission.HasCreateOrUpdatePermission);

            await _service.ProductPriceChangeAsync(dto);
            _controllerHelper.SetResponse(_response, "Product Price Changed Successfully.");

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Product, eUserPermission.HasDeletePermission);

            await _service.DeleteProductAsync(id);
            _controllerHelper.SetResponse(_response, $"Product with Id = {id} is Deleted Successfully.");

            return StatusCode(200, _response);
        }
    }
}