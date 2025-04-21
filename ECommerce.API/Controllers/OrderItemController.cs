using ECommerce.API.Helper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
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
            var data = await _service.GetAllOrderItemsAsync(requestParams);
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
        public async Task<IActionResult> GetOrderItemById(Guid id)
        {
            var data = await _service.GetOrderItemByIdAsync(id);
            if (data != null)
            {
                _response.Data = data;
                _response.Success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderItem([FromBody] OrderItemCreateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.OrderItem, eUserPermission.HasCreateOrUpdatePermission);

            await _service.CreateOrderItemAsync(dto);
            _response.Data = new { Message = "New Order Item Successfully." };
            _response.Success = true;

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrderItem([FromBody] OrderItemUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.OrderItem, eUserPermission.HasCreateOrUpdatePermission);

            dto.UserId = _userId;

            await _service.UpdateOrderItemAsync(dto);
            _response.Data = new { Message = "Order Item Modified Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }

        [HttpPut("UpdateQuantity")]
        public async Task<IActionResult> UpdateQuantity([FromBody] OrderItemQuantityUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.OrderItem, eUserPermission.HasCreateOrUpdatePermission);

            dto.UserId = _userId;

            await _service.UpdateQuantityAsync(dto);
            _response.Data = new { Message = "Quantity Modified Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }

        [HttpPut("UpdateUnitPrice")]
        public async Task<IActionResult> UpdateUnitPrice([FromBody] OrderItemUnitPriceUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.OrderItem, eUserPermission.HasCreateOrUpdatePermission);

            dto.UserId = _userId;

            await _service.UpdateUnitPriceAsync(dto);
            _response.Data = new { Message = "Unit Price Modified Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.OrderItem, eUserPermission.HasDeletePermission);

            await _service.DeleteOrderItemAsync(id);
            _response.Data = new { Message = $"Order Item with Id = {id} is Deleted Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }
    }
}