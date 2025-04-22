using ECommerce.API.Helper.Interfaces;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Enums;
using ECommerce.Shared.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _service;
        private readonly IControllerHelper _controllerHelper;

        public OrderController(IHTTPHelper httpHelper, IOrderService service, IControllerHelper controllerHelper)
            : base(httpHelper)
        {
            _service = service;
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromQuery] RequestParams requestParams)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Order, eUserPermission.HasFullPermission);

            var data = await _service.GetAllOrdersAsync(requestParams);
            _controllerHelper.SetResponse(_response, data, requestParams);

            return StatusCode(200, _response);
        }

        [HttpGet("GetOrdersForUser")]
        public async Task<IActionResult> GetOrdersForUser([FromQuery] RequestParams requestParams)
        {
            var data = await _service.GetAllOrdersAsync(requestParams, _userId);
            _controllerHelper.SetResponse(_response, data, requestParams);

            return StatusCode(200, _response);
        }

        [HttpGet("GetAllRecentOrders")]
        public async Task<IActionResult> GetAllRecentOrders([FromQuery] RequestParams requestParams)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Order, eUserPermission.HasFullPermission);

            var data = await _service.GetAllRecentOrdersAsync(requestParams);
            _controllerHelper.SetResponse(_response, data, requestParams);

            return StatusCode(200, _response);
        }

        [HttpGet("GetRecentOrdersForUser")]
        public async Task<IActionResult> GetRecentOrdersForUser([FromQuery] RequestParams requestParams)
        {
            var data = await _service.GetAllRecentOrdersAsync(requestParams, _userId);
            _controllerHelper.SetResponse(_response, data, requestParams);

            return StatusCode(200, _response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var data = await _service.GetOrderByIdAsync(id, _userId);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateFromCartDTO dto)
        {
            dto.UserId = _userId;

            await _service.CreateOrderAsync(dto);
            _controllerHelper.SetResponse(_response, "New Order Added Successfully.");

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Order, eUserPermission.HasCreateOrUpdatePermission);

            await _service.UpdateOrderAsync(dto);
            _controllerHelper.SetResponse(_response, "Order Modified Successfully.");

            return StatusCode(200, _response);
        }

        [HttpPut("UpdateOrderStatus")]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] OrderUpdateStatusDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Order, eUserPermission.HasCreateOrUpdatePermission);

            dto.UserId = _userId;

            await _service.UpdateOrderStatusAsync(dto);
            _controllerHelper.SetResponse(_response, "Order Status Modified Successfully.");

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Order, eUserPermission.HasDeletePermission);

            await _service.DeleteOrderAsync(id);
            _controllerHelper.SetResponse(_response, $"Order with Id = {id} is Deleted Successfully.");

            return StatusCode(200, _response);
        }
    }
}