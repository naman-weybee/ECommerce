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
    public class CartItemController : BaseController
    {
        private readonly ICartItemService _service;

        public CartItemController(ICartItemService service, IHTTPHelper httpHelper)
            : base(httpHelper)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCartItems([FromQuery] RequestParams requestParams, [FromQuery] bool isAdminSelf = false)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(CartItem).Name, eUserPermission.HasViewPermission);

            var response = new ResponseStructure();

            var isAdmin = User.IsInRole("Admin");
            var userId = (!isAdmin || (isAdmin && isAdminSelf)) ? _userId : default;

            var data = await _service.GetAllCartItemsAsync(requestParams, userId);
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
            await _httpHelper.ValidateUserAuthorization(typeof(CartItem).Name, eUserPermission.HasViewPermission);

            var response = new ResponseStructure();

            var data = await _service.GetCartItemByIdAsync(id, _userId);
            if (data != null)
            {
                response.data = data;
                response.success = true;
            }

            return StatusCode(200, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCartItem([FromBody] CartItemCreateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(CartItem).Name, eUserPermission.HasCreateOrUpdatePermission);

            var response = new ResponseStructure();

            dto.UserId = _userId;

            await _service.CreateCartItemAsync(dto);
            response.data = new { Message = "New Cart Item Added Successfully." };
            response.success = true;

            return StatusCode(201, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCartItem([FromBody] CartItemUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(CartItem).Name, eUserPermission.HasCreateOrUpdatePermission);

            var response = new ResponseStructure();

            dto.UserId = _userId;

            await _service.UpdateCartItemAsync(dto);
            response.data = new { Message = "Cart Item Modified Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpPut("UpdateQuantity")]
        public async Task<IActionResult> UpdateQuantity([FromBody] CartItemQuantityUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(CartItem).Name, eUserPermission.HasCreateOrUpdatePermission);

            var response = new ResponseStructure();

            dto.UserId = _userId;

            await _service.UpdateQuantityAsync(dto);
            response.data = new { Message = "Quantity Modified Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpPut("UpdateUnitPrice")]
        public async Task<IActionResult> UpdateUnitPrice([FromBody] CartItemUnitPriceUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(CartItem).Name, eUserPermission.HasCreateOrUpdatePermission);

            var response = new ResponseStructure();

            dto.UserId = _userId;

            await _service.UpdateUnitPriceAsync(dto);
            response.data = new { Message = "Unit Price Modified Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(CartItem).Name, eUserPermission.HasDeletePermission);

            var response = new ResponseStructure();

            await _service.DeleteCartItemAsync(id, _userId);
            response.data = new { Message = $"Cart Item with Id = {id} is Deleted Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }
    }
}