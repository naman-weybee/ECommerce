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
    public class OrderItemController : BaseController
    {
        private readonly IOrderItemService _service;

        public OrderItemController(IOrderItemService service, IHTTPHelper httpHelper)
            : base(httpHelper)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderItems([FromQuery] RequestParams requestParams)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(OrderItem).Name, eUserPermission.HasViewPermission);

            var response = new ResponseStructure();

            var data = await _service.GetAllOrderItemsAsync(requestParams);
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
        public async Task<IActionResult> GetOrderItemById(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(OrderItem).Name, eUserPermission.HasViewPermission);

            var response = new ResponseStructure();

            var data = await _service.GetOrderItemByIdAsync(id);
            if (data != null)
            {
                response.data = data;
                response.success = true;
            }

            return StatusCode(200, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderItem([FromBody] OrderItemCreateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(OrderItem).Name, eUserPermission.HasCreateOrUpdatePermission);

            var response = new ResponseStructure();

            await _service.CreateOrderItemAsync(dto);
            response.data = new { Message = "New Order Item Successfully." };
            response.success = true;

            return StatusCode(201, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrderItem([FromBody] OrderItemUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(OrderItem).Name, eUserPermission.HasCreateOrUpdatePermission);

            var response = new ResponseStructure();

            dto.UserId = _userId;

            await _service.UpdateOrderItemAsync(dto);
            response.data = new { Message = "Order Item Modified Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpPut("UpdateQuantity")]
        public async Task<IActionResult> UpdateQuantity([FromBody] OrderItemQuantityUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(OrderItem).Name, eUserPermission.HasCreateOrUpdatePermission);

            var response = new ResponseStructure();

            dto.UserId = _userId;

            await _service.UpdateQuantityAsync(dto);
            response.data = new { Message = "Quantity Modified Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpPut("UpdateUnitPrice")]
        public async Task<IActionResult> UpdateUnitPrice([FromBody] OrderItemUnitPriceUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(OrderItem).Name, eUserPermission.HasCreateOrUpdatePermission);

            var response = new ResponseStructure();

            dto.UserId = _userId;

            await _service.UpdateUnitPriceAsync(dto);
            response.data = new { Message = "Unit Price Modified Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(OrderItem).Name, eUserPermission.HasDeletePermission);

            var response = new ResponseStructure();

            await _service.DeleteOrderItemAsync(id);
            response.data = new { Message = $"Order Item with Id = {id} is Deleted Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }
    }
}