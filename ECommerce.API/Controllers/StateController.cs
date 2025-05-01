using ECommerce.API.Helper.Interfaces;
using ECommerce.Application.DTOs.State;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Enums;
using ECommerce.Shared.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class StateController : BaseController
    {
        private readonly IStateService _service;
        private readonly IControllerHelper _controllerHelper;

        public StateController(IHTTPHelper httpHelper, IStateService service, IControllerHelper controllerHelper)
            : base(httpHelper)
        {
            _service = service;
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStates([FromQuery] RequestParams requestParams)
        {
            var data = await _service.GetAllStatesAsync(requestParams, true);
            _controllerHelper.SetResponse(_response, data, requestParams);

            return StatusCode(200, _response);
        }

        [HttpGet("GetAllStatesByCountryId/{countryId}")]
        public async Task<IActionResult> GetAllStatesByCountryId(Guid countryId)
        {
            var data = await _service.GetAllStatesByCountryIdAsync(countryId, useQuery: true);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(200, _response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStateById(Guid id)
        {
            var data = await _service.GetStateByIdAsync(id, true);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertState([FromBody] StateUpsertDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.State, eUserPermission.HasCreateOrUpdatePermission);

            await _service.UpsertStateAsync(dto);
            _response.Data = new { Message = "State Saved Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteState(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.State, eUserPermission.HasDeletePermission);

            await _service.DeleteStateAsync(id);
            _controllerHelper.SetResponse(_response, $"State with Id = {id} is Deleted Successfully.");

            return StatusCode(200, _response);
        }
    }
}