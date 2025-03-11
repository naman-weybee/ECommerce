using ECommerce.API.Helper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Shared.RequestModel;
using ECommerce.Shared.ResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class AddressController : BaseController
    {
        private readonly IAddressService _service;

        public AddressController(IAddressService service, IHTTPHelper httpHelper)
            : base(httpHelper)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAddresses([FromQuery] RequestParams requestParams, [FromQuery] bool isAdminSelf = false)
        {
            var isAdmin = User.IsInRole("Admin");
            var userId = (!isAdmin || (isAdmin && isAdminSelf)) ? _userId : default;

            var data = await _service.GetAllAddressesAsync(requestParams, userId);
            if (data != null)
            {
                _response.data = new ResponseMetadata<object>()
                {
                    page_number = requestParams.pageNumber,
                    page_size = requestParams.pageSize,
                    records = data,
                    total_records_count = requestParams.recordCount
                };

                _response.success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressById(Guid id)
        {
            var data = await _service.GetAddressByIdAsync(id, _userId);
            if (data != null)
            {
                _response.data = data;
                _response.success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] AddressCreateDTO dto)
        {
            dto.UserId = _userId;

            await _service.CreateAddressAsync(dto);
            _response.data = new { Message = "New Address Added Successfully." };
            _response.success = true;

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAddress([FromBody] AddressUpdateDTO dto)
        {
            dto.UserId = _userId;

            await _service.UpdateAddressAsync(dto);
            _response.data = new { Message = "Address Modified Successfully." };
            _response.success = true;

            return StatusCode(200, _response);
        }

        [HttpPut("UpdateAddressType")]
        public async Task<IActionResult> UpdateAddressType([FromBody] AddressTypeUpdateDTO dto)
        {
            dto.UserId = _userId;

            await _service.UpdateAddressTypeAsync(dto);
            _response.data = new { Message = "Address Type Updated Successfully." };
            _response.success = true;

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            await _service.DeleteAddressAsync(id, _userId);
            _response.data = new { Message = $"Address with Id = {id} is Deleted Successfully." };
            _response.success = true;

            return StatusCode(200, _response);
        }
    }
}