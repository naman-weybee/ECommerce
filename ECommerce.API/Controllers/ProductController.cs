using ECommerce.API.Helper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Shared.RequestModel;
using ECommerce.Shared.ResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _service;

        public ProductController(IProductService service, IHTTPHelper httpHelper)
            : base(httpHelper)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] RequestParams requestParams)
        {
            var data = await _service.GetAllProductsAsync(requestParams);
            if (data != null)
            {
                _response.data = new ResponseMetadata<object>()
                {
                    page_number = requestParams.pageNumber,
                    page_size = requestParams.pageSize,
                    records = data,
                    total_records_count = requestParams.recordCount
                };

                _response.success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var data = await _service.GetProductByIdAsync(id);
            if (data != null)
            {
                _response.data = data;
                _response.success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Product, eUserPermission.HasCreateOrUpdatePermission);

            await _service.CreateProductAsync(dto);
            _response.data = new { Message = "New Product Added Successfully." };
            _response.success = true;

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Product, eUserPermission.HasCreateOrUpdatePermission);

            await _service.UpdateProductAsync(dto);
            _response.data = new { Message = "Product Modified Successfully." };
            _response.success = true;

            return StatusCode(200, _response);
        }

        [HttpPut("IncreaseStock")]
        public async Task<IActionResult> IncreaseStock([FromBody] ProductStockChangeDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Product, eUserPermission.HasCreateOrUpdatePermission);

            await _service.ProductStockChangeAsync(dto.Id, dto.Quantity, true);
            _response.data = new { Message = "Product Stock Increased Successfully." };
            _response.success = true;

            return StatusCode(200, _response);
        }

        [HttpPut("DecreaseStock")]
        public async Task<IActionResult> DecreaseStock([FromBody] ProductStockChangeDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Product, eUserPermission.HasCreateOrUpdatePermission);

            await _service.ProductStockChangeAsync(dto.Id, dto.Quantity, false);
            _response.data = new { Message = "Product Stock Decreased Successfully." };
            _response.success = true;

            return StatusCode(200, _response);
        }

        [HttpPut("PriceChange")]
        public async Task<IActionResult> PriceChange([FromBody] ProductPriceChangeDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Product, eUserPermission.HasCreateOrUpdatePermission);

            await _service.ProductPriceChangeAsync(dto);
            _response.data = new { Message = "Product Price Changed Successfully." };
            _response.success = true;

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Product, eUserPermission.HasDeletePermission);

            await _service.DeleteProductAsync(id);
            _response.data = new { Message = $"Product with Id = {id} is Deleted Successfully." };
            _response.success = true;

            return StatusCode(200, _response);
        }
    }
}