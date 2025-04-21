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
        public async Task<IActionResult> GetAddressById(Guid id)
        {
            var data = await _service.GetAddressByIdAsync(id, _userId);
            if (data != null)
            {
                _response.Data = data;
                _response.Success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] AddressCreateDTO dto)
        {
            dto.UserId = _userId;

            await _service.CreateAddressAsync(dto);
            _response.Data = new { Message = "New Address Added Successfully." };
            _response.Success = true;

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAddress([FromBody] AddressUpdateDTO dto)
        {
            dto.UserId = _userId;

            await _service.UpdateAddressAsync(dto);
            _response.Data = new { Message = "Address Modified Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }

        [HttpPut("UpdateAddressType")]
        public async Task<IActionResult> UpdateAddressType([FromBody] AddressTypeUpdateDTO dto)
        {
            dto.UserId = _userId;

            await _service.UpdateAddressTypeAsync(dto);
            _response.Data = new { Message = "Address Type Updated Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            await _service.DeleteAddressAsync(id, _userId);
            _response.Data = new { Message = $"Address with Id = {id} is Deleted Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }
    }
}