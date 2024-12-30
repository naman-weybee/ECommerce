using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Shared.RequestModel;
using ECommerce.Shared.ResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AddressController : Controller
    {
        private readonly IAddressService _service;
        private readonly ILogger<AddressController> _logger;

        public AddressController(IAddressService service, ILogger<AddressController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAddresses([FromQuery] RequestParams requestParams)
        {
            var response = new ResponseStructure();

            try
            {
                var data = await _service.GetAllAddressesAsync(requestParams);
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
        public async Task<IActionResult> GetAddressById(Guid id)
        {
            var response = new ResponseStructure();

            try
            {
                var data = await _service.GetAddressByIdAsync(id);
                if (data != null)
                {
                    response.data = data;
                    response.success = true;
                    return StatusCode(200, data);
                }

                response.error = $"Requested Address for Id = {id} is Not Found...!";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
                _logger.LogError(ex.Message);

                return StatusCode(500, response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] AddressCreateDTO addressDTO)
        {
            var response = new ResponseStructure();

            try
            {
                await _service.CreateAddressAsync(addressDTO);
                response.data = new { Message = "New Address Added Successfully...!" };
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
        public async Task<IActionResult> UpdateAddress([FromBody] AddressUpdateDTO addressDTO)
        {
            var response = new ResponseStructure();

            try
            {
                await _service.UpdateAddressAsync(addressDTO);
                response.data = new { Message = "Address Modified Successfully...!" };
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
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            var response = new ResponseStructure();

            try
            {
                await _service.DeleteAddressAsync(id);
                response.data = new { Message = $"Address with Id = {id} is Deleted Successfully...!" };
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