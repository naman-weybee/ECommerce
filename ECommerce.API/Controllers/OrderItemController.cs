using ECommerce.API.Helper.Interfaces;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Enums;
using ECommerce.Shared.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class OrderItemController : BaseController
    {
        private readonly IOrderItemService _service;
        private readonly IControllerHelper _controllerHelper;

        public OrderItemController(IHTTPHelper httpHelper, IOrderItemService service, IControllerHelper controllerHelper)
            : base(httpHelper)
        {
            _service = service;
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderItems([FromQuery] RequestParams requestParams)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.OrderItem, eUserPermission.HasFullPermission);

            var data = await _service.GetAllOrderItemsAsync(requestParams);
            _controllerHelper.SetResponse(_response, data, requestParams);

            return StatusCode(200, _response);
        }

        [HttpGet("GetOrderItemsByOrder")]
        public async Task<IActionResult> GetOrderItemsByOrder([FromQuery] RequestParams requestParams, [FromQuery] Guid orderId)
        {
            var data = await _service.GetOrderItemsByOrderAsync(orderId, requestParams);
            _controllerHelper.SetResponse(_response, data, requestParams);

            return StatusCode(200, _response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderItemById(Guid id)
        {
            var data = await _service.GetOrderItemByIdAsync(id);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertOrderItem([FromBody] OrderItemUpsertDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.OrderItem, eUserPermission.HasCreateOrUpdatePermission);

            await _service.UpsertOrderItemAsync(dto);
            _controllerHelper.SetResponse(_response, "Order Saved Successfully.");

            return StatusCode(200, _response);
        }

        [HttpPut("UpdateQuantity")]
        public async Task<IActionResult> UpdateQuantity([FromBody] OrderItemQuantityUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.OrderItem, eUserPermission.HasCreateOrUpdatePermission);

            dto.UserId = _userId;

            await _service.UpdateQuantityAsync(dto);
            _controllerHelper.SetResponse(_response, "Quantity Modified Successfully.");

            return StatusCode(200, _response);
        }

        [HttpPut("UpdateUnitPrice")]
        public async Task<IActionResult> UpdateUnitPrice([FromBody] OrderItemUnitPriceUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.OrderItem, eUserPermission.HasCreateOrUpdatePermission);

            dto.UserId = _userId;

            _service.UpdateUnitPrice(dto);
            _controllerHelper.SetResponse(_response, "Unit Price Modified Successfully.");

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.OrderItem, eUserPermission.HasDeletePermission);

            await _service.DeleteOrderItemAsync(id);
            _controllerHelper.SetResponse(_response, $"Order Item with Id = {id} is Deleted Successfully.");

            return StatusCode(200, _response);
        }
    }
}