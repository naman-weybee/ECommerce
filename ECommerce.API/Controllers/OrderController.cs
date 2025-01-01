using ECommerce.API.Filters;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Shared.RequestModel;
using ECommerce.Shared.ResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromQuery] RequestParams requestParams)
        {
            var response = new ResponseStructure();

            var data = await _service.GetAllOrdersAsync(requestParams);
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
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var response = new ResponseStructure();

            var data = await _service.GetOrderByIdAsync(id);
            if (data != null)
            {
                response.data = data;
                response.success = true;
                return StatusCode(200, data);
            }

            response.error = $"Requested Order for Id = {id} is Not Found.";
            return NotFound(response);
        }

        [HttpPost]
        [ServiceFilter(typeof(ExecutionFilter))]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDTO dto)
        {
            var response = new ResponseStructure();

            await _service.CreateOrderAsync(dto);
            response.data = new { Message = "New Order Added Successfully." };
            response.success = true;
            return StatusCode(201, response);
        }

        [HttpPut]
        [ServiceFilter(typeof(ExecutionFilter))]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderUpdateDTO dto)
        {
            var response = new ResponseStructure();

            await _service.UpdateOrderAsync(dto);
            response.data = new { Message = "Order Modified Successfully." };
            response.success = true;
            return StatusCode(200, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var response = new ResponseStructure();

            await _service.DeleteOrderAsync(id);
            response.data = new { Message = $"Order with Id = {id} is Deleted Successfully." };
            response.success = true;
            return StatusCode(200, response);
        }
    }
}