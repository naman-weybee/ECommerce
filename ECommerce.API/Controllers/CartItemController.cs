using ECommerce.API.Helper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
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
            var isAdmin = User.IsInRole("Admin");
            var userId = (!isAdmin || (isAdmin && isAdminSelf)) ? _userId : default;

            var data = await _service.GetAllCartItemsAsync(requestParams, userId);
            if (data != null)
            {
                _response.Data = new ResponseMetadata<object>()
                {
                    Page_Number = requestParams.PageNumber,
                    Page_Size = requestParams.PageSize,
                    Records = data,
                    Total_Records_Count = requestParams.RecordCount
                };

                _response.Success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartItemById(Guid id)
        {
            var data = await _service.GetCartItemByIdAsync(id, _userId);
            if (data != null)
            {
                _response.Data = data;
                _response.Success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCartItem([FromBody] CartItemCreateDTO dto)
        {
            dto.UserId = _userId;

            await _service.CreateCartItemAsync(dto);
            _response.Data = new { Message = "New Cart Item Added Successfully." };
            _response.Success = true;

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCartItem([FromBody] CartItemUpdateDTO dto)
        {
            dto.UserId = _userId;

            await _service.UpdateCartItemAsync(dto);
            _response.Data = new { Message = "Cart Item Modified Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }

        [HttpPut("UpdateQuantity")]
        public async Task<IActionResult> UpdateQuantity([FromBody] CartItemQuantityUpdateDTO dto)
        {
            dto.UserId = _userId;

            await _service.UpdateQuantityAsync(dto);
            _response.Data = new { Message = "Quantity Modified Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }

        [HttpPut("UpdateUnitPrice")]
        public async Task<IActionResult> UpdateUnitPrice([FromBody] CartItemUnitPriceUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.CartItem, eUserPermission.HasFullPermission);

            dto.UserId = _userId;

            await _service.UpdateUnitPriceAsync(dto);
            _response.Data = new { Message = "Unit Price Modified Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(Guid id)
        {
            await _service.DeleteCartItemAsync(id, _userId);
            _response.Data = new { Message = $"Cart Item with Id = {id} is Deleted Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }
    }
}