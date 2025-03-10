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
    public class OrderController : BaseController
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service, IHTTPHelper httpHelper)
            : base(httpHelper)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromQuery] RequestParams requestParams, [FromQuery] bool isAdminSelf = false)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Order).Name, eUserPermission.HasViewPermission);

            var response = new ResponseStructure();

            var isAdmin = User.IsInRole("Admin");
            var userId = (!isAdmin || (isAdmin && isAdminSelf)) ? _userId : default;

            var data = await _service.GetAllOrdersAsync(requestParams, userId);
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

        [HttpGet("GetAllRecentOrders")]
        public async Task<IActionResult> GetAllRecentOrders([FromQuery] RequestParams requestParams, [FromQuery] bool isAdminSelf = false)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Order).Name, eUserPermission.HasViewPermission);

            var response = new ResponseStructure();

            var isAdmin = User.IsInRole("Admin");
            var userId = (!isAdmin || (isAdmin && isAdminSelf)) ? _userId : default;

            var data = await _service.GetAllRecentOrdersAsync(requestParams, userId);
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
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Order).Name, eUserPermission.HasViewPermission);

            var response = new ResponseStructure();

            var data = await _service.GetOrderByIdAsync(id, _userId);
            if (data != null)
            {
                response.data = data;
                response.success = true;
            }

            return StatusCode(200, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateFromCartDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Order).Name, eUserPermission.HasCreateOrUpdatePermission);

            var response = new ResponseStructure();

            dto.UserId = _userId;

            await _service.CreateOrderAsync(dto);
            response.data = new { Message = "New Order Added Successfully." };
            response.success = true;

            return StatusCode(201, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Order).Name, eUserPermission.HasCreateOrUpdatePermission);

            var response = new ResponseStructure();

            await _service.UpdateOrderAsync(dto);
            response.data = new { Message = "Order Modified Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpPut("UpdateOrderStatus")]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] OrderUpdateStatusDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Order).Name, eUserPermission.HasCreateOrUpdatePermission);

            var response = new ResponseStructure();

            dto.UserId = _userId;

            await _service.UpdateOrderStatusAsync(dto);
            response.data = new { Message = "Order Status Modified Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Order).Name, eUserPermission.HasDeletePermission);

            var response = new ResponseStructure();

            await _service.DeleteOrderAsync(id);
            response.data = new { Message = $"Order with Id = {id} is Deleted Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }
    }
}