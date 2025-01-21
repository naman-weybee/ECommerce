using ECommerce.API.Helper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Shared.RequestModel;
using ECommerce.Shared.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Authorize]
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

            var userId = HTTPHelper.GetUserId();

            var data = await _service.GetAllAddressesAsync(requestParams, userId);
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
        public async Task<IActionResult> GetAddressById(Guid id)
        {
            var response = new ResponseStructure();

            var userId = HTTPHelper.GetUserId();

            var data = await _service.GetAddressByIdAsync(id, userId);
            if (data != null)
            {
                response.data = data;
                response.success = true;
            }

            return StatusCode(200, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] AddressCreateDTO dto)
        {
            var response = new ResponseStructure();

            dto.UserId = HTTPHelper.GetUserId();

            await _service.CreateAddressAsync(dto);
            response.data = new { Message = "New Address Added Successfully." };
            response.success = true;

            return StatusCode(201, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAddress([FromBody] AddressUpdateDTO dto)
        {
            var response = new ResponseStructure();

            dto.UserId = HTTPHelper.GetUserId();

            await _service.UpdateAddressAsync(dto);
            response.data = new { Message = "Address Modified Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpPut("UpdateAddressType")]
        public async Task<IActionResult> UpdateAddressType([FromBody] AddressTypeUpdateDTO dto)
        {
            var response = new ResponseStructure();

            dto.UserId = HTTPHelper.GetUserId();

            await _service.UpdateAddressTypeAsync(dto);
            response.data = new { Message = "Address Type Updated Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            var response = new ResponseStructure();

            var userId = HTTPHelper.GetUserId();

            await _service.DeleteAddressAsync(id, userId);
            response.data = new { Message = $"Address with Id = {id} is Deleted Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }
    }
}