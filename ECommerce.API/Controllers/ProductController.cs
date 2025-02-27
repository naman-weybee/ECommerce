﻿using ECommerce.API.Helper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Shared.RequestModel;
using ECommerce.Shared.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Authorize]
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
            var response = new ResponseStructure();

            var data = await _service.GetAllProductsAsync(requestParams);
            if (data != null)
            {
                response.data = new ResponseMetadata<object>()
                {
                    page_number = requestParams.pageNumber,
                    page_size = requestParams.pageSize,
                    records = data,
                    total_records_count = requestParams.recordCount
                };

                response.success = true;
            }

            return StatusCode(200, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var response = new ResponseStructure();

            var data = await _service.GetProductByIdAsync(id);
            if (data != null)
            {
                response.data = data;
                response.success = true;
            }

            return StatusCode(200, response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDTO dto)
        {
            var response = new ResponseStructure();

            await _service.CreateProductAsync(dto);
            response.data = new { Message = "New Product Added Successfully." };
            response.success = true;

            return StatusCode(201, response);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDTO dto)
        {
            var response = new ResponseStructure();

            await _service.UpdateProductAsync(dto);
            response.data = new { Message = "Product Modified Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpPut("IncreaseStock")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> IncreaseStock([FromBody] ProductStockChangeDTO dto)
        {
            var response = new ResponseStructure();

            await _service.ProductStockChangeAsync(dto.Id, dto.Quantity, true);
            response.data = new { Message = "Product Stock Increased Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpPut("DecreaseStock")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DecreaseStock([FromBody] ProductStockChangeDTO dto)
        {
            var response = new ResponseStructure();

            await _service.ProductStockChangeAsync(dto.Id, dto.Quantity, false);
            response.data = new { Message = "Product Stock Decreased Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpPut("PriceChange")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PriceChange([FromBody] ProductPriceChangeDTO dto)
        {
            var response = new ResponseStructure();

            await _service.ProductPriceChangeAsync(dto);
            response.data = new { Message = "Product Price Changed Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var response = new ResponseStructure();

            await _service.DeleteProductAsync(id);
            response.data = new { Message = $"Product with Id = {id} is Deleted Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }
    }
}