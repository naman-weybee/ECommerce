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
    public class StateController : BaseController
    {
        private readonly IStateService _service;

        public StateController(IStateService service, IHTTPHelper httpHelper)
            : base(httpHelper)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStates([FromQuery] RequestParams requestParams)
        {
            var data = await _service.GetAllStatesAsync(requestParams);
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

        [HttpGet("GetAllStatesByCountryId/{countryId}")]
        public async Task<IActionResult> GetAllStatesByCountryId(Guid countryId)
        {
            var data = await _service.GetAllStatesByCountryIdAsync(countryId);
            if (data != null)
            {
                _response.data = new ResponseMetadata<object>()
                {
                    page_number = 1,
                    page_size = data.Count,
                    records = data,
                    total_records_count = 1
                };

                _response.success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStateById(Guid id)
        {
            var data = await _service.GetStateByIdAsync(id);
            if (data != null)
            {
                _response.data = data;
                _response.success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateState([FromBody] StateCreateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.State, eUserPermission.HasCreateOrUpdatePermission);

            await _service.CreateStateAsync(dto);
            _response.data = new { Message = "New State Added Successfully." };
            _response.success = true;

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateState([FromBody] StateUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.State, eUserPermission.HasCreateOrUpdatePermission);

            await _service.UpdateStateAsync(dto);
            _response.data = new { Message = "State Modified Successfully." };
            _response.success = true;

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteState(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.State, eUserPermission.HasDeletePermission);

            await _service.DeleteStateAsync(id);
            _response.data = new { Message = $"State with Id = {id} is Deleted Successfully." };
            _response.success = true;

            return StatusCode(200, _response);
        }
    }
}