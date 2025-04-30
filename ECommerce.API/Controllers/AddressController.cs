using ECommerce.API.Attributes;
using ECommerce.API.Helper.Interfaces;
using ECommerce.Application.DTOs.Address;
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

        [HttpGet("GetAllAddressesByUser")]
        public async Task<IActionResult> GetAllAddressesByUser([FromQuery] RequestParams requestParams)
        {
            var data = await _service.GetAllAddressesByUserAsync(_userId, requestParams);
            _controllerHelper.SetResponse(_response, data, requestParams);

            return StatusCode(200, _response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressById(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Address, eUserPermission.HasFullPermission);

            var data = await _service.GetAddressByIdAsync(id);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(200, _response);
        }

        [HttpGet("GetSpecificAddressByUser/{id}")]
        public async Task<IActionResult> GetSpecificAddressByUser(Guid id)
        {
            var data = await _service.GetSpecificAddressByUserAsync(id, _userId);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertAddress([FromBody] AddressUpsertDTO dto)
        {
            dto.UserId = _userId;

            await _service.UpsertAddressAsync(dto);
            _controllerHelper.SetResponse(_response, "Address Saved Successfully.");

            return StatusCode(200, _response);
        }

        [BypassDbTransection]
        [HttpPut("UpdateAddressType")]
        public async Task<IActionResult> UpdateAddressType([FromBody] AddressTypeUpdateDTO dto)
        {
            dto.UserId = _userId;

            await _service.UpdateAddressTypeAsync(dto);
            _controllerHelper.SetResponse(_response, "Address Type Updated Successfully.");

            return StatusCode(200, _response);
        }

        [HttpDelete("DeleteAddressByUser/{id}")]
        public async Task<IActionResult> DeleteAddressByUser(Guid id)
        {
            await _service.DeleteAddressByUserAsync(id, _userId);
            _controllerHelper.SetResponse(_response, $"Address with Id = {id} is Deleted Successfully.");

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Address, eUserPermission.HasFullPermission);

            await _service.DeleteAddressAsync(id);
            _controllerHelper.SetResponse(_response, $"Address with Id = {id} is Deleted Successfully.");

            return StatusCode(200, _response);
        }
    }
}