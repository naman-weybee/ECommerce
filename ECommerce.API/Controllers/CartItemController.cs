using ECommerce.API.Helper.Interfaces;
using ECommerce.Application.DTOs.CartItem;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Enums;
using ECommerce.Shared.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class CartItemController : BaseController
    {
        private readonly ICartItemService _service;
        private readonly IControllerHelper _controllerHelper;

        public CartItemController(IHTTPHelper httpHelper, ICartItemService service, IControllerHelper controllerHelper)
            : base(httpHelper)
        {
            _service = service;
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCartItems([FromQuery] RequestParams requestParams)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.CartItem, eUserPermission.HasFullPermission);

            var data = await _service.GetAllCartItemsAsync(requestParams);
            _controllerHelper.SetResponse(_response, data, requestParams);

            return StatusCode(200, _response);
        }

        [HttpGet("GetAllCartItemsByUser")]
        public async Task<IActionResult> GetAllCartItemsByUser([FromQuery] RequestParams requestParams)
        {
            var data = await _service.GetAllCartItemsByUserAsync(_userId, requestParams);
            _controllerHelper.SetResponse(_response, data, requestParams);

            return StatusCode(200, _response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartItemById(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.CartItem, eUserPermission.HasFullPermission);

            var data = await _service.GetCartItemByIdAsync(id);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(200, _response);
        }

        [HttpGet("GetSpecificCartItemByUser/{id}")]
        public async Task<IActionResult> GetSpecificCartItemByUser(Guid id)
        {
            var data = await _service.GetSpecificCartItemsByUserAsync(id, _userId);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCartItem([FromBody] CartItemCreateDTO dto)
        {
            dto.UserId = _userId;

            await _service.CreateCartItemAsync(dto);
            _controllerHelper.SetResponse(_response, "New Cart Item Added Successfully.");

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCartItem([FromBody] CartItemUpdateDTO dto)
        {
            dto.UserId = _userId;

            await _service.UpdateCartItemAsync(dto);
            _controllerHelper.SetResponse(_response, "Cart Item Modified Successfully.");

            return StatusCode(200, _response);
        }

        [HttpPut("UpdateQuantity")]
        public async Task<IActionResult> UpdateQuantity([FromBody] CartItemQuantityUpdateDTO dto)
        {
            dto.UserId = _userId;

            await _service.UpdateQuantityAsync(dto);
            _controllerHelper.SetResponse(_response, "Quantity Modified Successfully.");

            return StatusCode(200, _response);
        }

        [HttpPut("UpdateUnitPrice")]
        public async Task<IActionResult> UpdateUnitPrice([FromBody] CartItemUnitPriceUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.CartItem, eUserPermission.HasFullPermission);

            dto.UserId = _userId;

            await _service.UpdateUnitPriceAsync(dto);
            _controllerHelper.SetResponse(_response, "Unit Price Modified Successfully.");

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(Guid id)
        {
            await _service.DeleteCartItemAsync(id, _userId);
            _controllerHelper.SetResponse(_response, $"Cart Item with Id = {id} is Deleted Successfully.");

            return StatusCode(200, _response);
        }
    }
}