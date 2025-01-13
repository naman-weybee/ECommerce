using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Shared.RequestModel;
using ECommerce.Shared.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Authorize]
    public class CartItemController : Controller
    {
        private readonly ICartItemService _service;

        public CartItemController(ICartItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCartItems([FromQuery] RequestParams requestParams)
        {
            var response = new ResponseStructure();

            var data = await _service.GetAllCartItemsAsync(requestParams);
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
        public async Task<IActionResult> GetCartItemById(Guid id)
        {
            var response = new ResponseStructure();

            var data = await _service.GetCartItemByIdAsync(id);
            if (data != null)
            {
                response.data = data;
                response.success = true;
            }

            return StatusCode(200, response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCartItem([FromBody] CartItemCreateDTO dto)
        {
            var response = new ResponseStructure();

            await _service.CreateCartItemAsync(dto);
            response.data = new { Message = "New Cart Item Added Successfully." };
            response.success = true;

            return StatusCode(201, response);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCartItem([FromBody] CartItemUpdateDTO dto)
        {
            var response = new ResponseStructure();

            await _service.UpdateCartItemAsync(dto);
            response.data = new { Message = "Cart Item Modified Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpPut("UpdateQuantity/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateQuantity(Guid id, [FromBody] CartItemQuantityUpdateDTO dto)
        {
            var response = new ResponseStructure();

            await _service.UpdateQuantityAsync(dto);
            response.data = new { Message = "Quantity Modified Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpPut("UpdateUnitPrice/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUnitPrice(Guid id, [FromBody] CartItemUnitPriceUpdateDTO dto)
        {
            var response = new ResponseStructure();

            await _service.UpdateUnitPriceAsync(dto);
            response.data = new { Message = "Unit Price Modified Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCartItem(Guid id)
        {
            var response = new ResponseStructure();

            await _service.DeleteCartItemAsync(id);
            response.data = new { Message = $"Cart Item with Id = {id} is Deleted Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }
    }
}