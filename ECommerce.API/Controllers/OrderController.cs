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
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService service, ILogger<OrderController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromQuery] RequestParams requestParams)
        {
            var response = new ResponseStructure();

            try
            {
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
            catch (Exception ex)
            {
                response.error = ex.Message;
                _logger.LogError(ex.Message);

                return StatusCode(500, response);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById([FromRoute] Guid id)
        {
            var response = new ResponseStructure();

            try
            {
                var data = await _service.GetOrderByIdAsync(id);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                    return StatusCode(200, data);
                }

                response.error = $"Requested Order for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                _logger.LogError(ex.Message);

                return StatusCode(500, response);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateOrder([FromForm] OrderCreateDTO dto)
        {
            var response = new ResponseStructure();

            try
            {
                await _service.CreateOrderAsync(dto);
                response.data = new { Message = "New Order Added Successfully...!" };
                response.success = true;
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                _logger.LogError(ex.Message);

                return StatusCode(500, response);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromForm] OrderUpdateDTO dto)
        {
            var response = new ResponseStructure();

            try
            {
                await _service.UpdateOrderAsync(dto);
                response.data = new { Message = "Order Modified Successfully...!" };
                response.success = true;
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                _logger.LogError(ex.Message);

                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] Guid id)
        {
            var response = new ResponseStructure();

            try
            {
                await _service.DeleteOrderAsync(id);
                response.data = new { Message = $"Order with Id = {id} is Deleted Successfully...!" };
                response.success = true;
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                _logger.LogError(ex.Message);

                return StatusCode(500, response);
            }
        }
    }
}