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
    public class AddressController : Controller
    {
        private readonly IAddressService _service;

        public AddressController(IAddressService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAddresses([FromQuery] RequestParams requestParams)
        {
            var response = new ResponseStructure();

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressById(Guid id)
        {
            var response = new ResponseStructure();

            var data = await _service.GetAddressByIdAsync(id);
            if (data != null)
            {
                response.data = data;
                response.success = true;
                return StatusCode(200, data);
            }

            response.error = $"Requested Address for Id = {id} is Not Found.";
            return NotFound(response);
        }

        [HttpPost]
        [ServiceFilter(typeof(ExecutionFilter))]
        public async Task<IActionResult> CreateAddress([FromBody] AddressCreateDTO addressDTO)
        {
            var response = new ResponseStructure();

            await _service.CreateAddressAsync(addressDTO);
            response.data = new { Message = "New Address Added Successfully." };
            response.success = true;
            return StatusCode(201, response);
        }

        [HttpPut]
        [ServiceFilter(typeof(ExecutionFilter))]
        public async Task<IActionResult> UpdateAddress([FromBody] AddressUpdateDTO addressDTO)
        {
            var response = new ResponseStructure();

            await _service.UpdateAddressAsync(addressDTO);
            response.data = new { Message = "Address Modified Successfully." };
            response.success = true;
            return StatusCode(200, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            var response = new ResponseStructure();

            await _service.DeleteAddressAsync(id);
            response.data = new { Message = $"Address with Id = {id} is Deleted Successfully." };
            response.success = true;
            return StatusCode(200, response);
        }
    }
}