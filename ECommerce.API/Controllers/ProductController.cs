﻿using ECommerce.API.Filters;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Shared.RequestModel;
using ECommerce.Shared.ResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _service;

        public ProductController(IProductService service, ILogger<ProductController> logger)
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

            return Ok(response);
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
                return StatusCode(200, data);
            }

            response.error = $"Requested Product for Id = {id} is Not Found.";
            return NotFound(response);
        }

        [HttpPost]
        [ServiceFilter(typeof(ExecutionFilter))]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDTO productDTO)
        {
            var response = new ResponseStructure();

            await _service.CreateProductAsync(productDTO);
            response.data = new { Message = "New Product Added Successfully." };
            response.success = true;
            return StatusCode(201, response);
        }

        [HttpPut]
        [ServiceFilter(typeof(ExecutionFilter))]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDTO productDTO)
        {
            var response = new ResponseStructure();

            await _service.UpdateProductAsync(productDTO);
            response.data = new { Message = "Product Modified Successfully." };
            response.success = true;
            return StatusCode(200, response);
        }

        [HttpDelete("{id}")]
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