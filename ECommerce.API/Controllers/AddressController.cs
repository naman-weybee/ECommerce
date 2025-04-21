using ECommerce.API.Helper.Interfaces;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Enums;
using ECommerce.Shared.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class AddressController : BaseController
    {
        private readonly IAddressService _service;
        private readonly IControllerHelper _controllerHelper;

        public AddressController(IHTTPHelper httpHelper, IAddressService service, IControllerHelper controllerHelper)
            : base(httpHelper)
        {
            _service = service;
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAddresses([FromQuery] RequestParams requestParams)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Address, eUserPermission.HasFullPermission);

            var data = await _service.GetAllAddressesAsync(requestParams);
            _controllerHelper.SetResponse(_response, data, requestParams);

            return StatusCode(200, _response);
        }

        [HttpGet("GetAddressesForUser")]
        public async Task<IActionResult> GetAddressesForUser([FromQuery] RequestParams requestParams)
        {
            var data = await _service.GetAllAddressesAsync(requestParams, _userId);
            _controllerHelper.SetResponse(_response, data, requestParams);

            return StatusCode(200, _response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressById(Guid id)
        {
            var data = await _service.GetAddressByIdAsync(id, _userId);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] AddressCreateDTO dto)
        {
            dto.UserId = _userId;

            await _service.CreateAddressAsync(dto);
            _controllerHelper.SetResponse(_response, "New Address Added Successfully.");

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAddress([FromBody] AddressUpdateDTO dto)
        {
            dto.UserId = _userId;

            await _service.UpdateAddressAsync(dto);
            _controllerHelper.SetResponse(_response, "Address Modified Successfully.");

            return StatusCode(200, _response);
        }

        [HttpPut("UpdateAddressType")]
        public async Task<IActionResult> UpdateAddressType([FromBody] AddressTypeUpdateDTO dto)
        {
            dto.UserId = _userId;

            await _service.UpdateAddressTypeAsync(dto);
            _controllerHelper.SetResponse(_response, "Address Type Updated Successfully.");

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            await _service.DeleteAddressAsync(id, _userId);
            _controllerHelper.SetResponse(_response, $"Address with Id = {id} is Deleted Successfully.");

            return StatusCode(200, _response);
        }
    }
}