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
            await _httpHelper.ValidateUserAuthorization(typeof(Address).Name, eUserPermission.HasViewPermission);

            var response = new ResponseStructure();

            var isAdmin = User.IsInRole("Admin");
            var userId = (!isAdmin || (isAdmin && isAdminSelf)) ? _userId : default;

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
            await _httpHelper.ValidateUserAuthorization(typeof(Address).Name, eUserPermission.HasViewPermission);

            var response = new ResponseStructure();

            var data = await _service.GetAddressByIdAsync(id, _userId);
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
            await _httpHelper.ValidateUserAuthorization(typeof(Address).Name, eUserPermission.HasCreateOrUpdatePermission);

            var response = new ResponseStructure();

            dto.UserId = _userId;

            await _service.CreateAddressAsync(dto);
            response.data = new { Message = "New Address Added Successfully." };
            response.success = true;

            return StatusCode(201, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAddress([FromBody] AddressUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Address).Name, eUserPermission.HasCreateOrUpdatePermission);

            var response = new ResponseStructure();

            dto.UserId = _userId;

            await _service.UpdateAddressAsync(dto);
            response.data = new { Message = "Address Modified Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpPut("UpdateAddressType")]
        public async Task<IActionResult> UpdateAddressType([FromBody] AddressTypeUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Address).Name, eUserPermission.HasCreateOrUpdatePermission);

            var response = new ResponseStructure();

            dto.UserId = _userId;

            await _service.UpdateAddressTypeAsync(dto);
            response.data = new { Message = "Address Type Updated Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Address).Name, eUserPermission.HasDeletePermission);

            var response = new ResponseStructure();

            await _service.DeleteAddressAsync(id, _userId);
            response.data = new { Message = $"Address with Id = {id} is Deleted Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }
    }
}