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
            await _httpHelper.ValidateUserAuthorization(typeof(State).Name, eUserPermission.HasViewPermission);

            var response = new ResponseStructure();

            var data = await _service.GetAllStatesAsync(requestParams);
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

        [HttpGet("GetAllStatesByCountryId/{countryId}")]
        public async Task<IActionResult> GetAllStatesByCountryId(Guid countryId)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(State).Name, eUserPermission.HasViewPermission);

            var response = new ResponseStructure();

            var data = await _service.GetAllStatesByCountryIdAsync(countryId);
            if (data != null)
            {
                response.data = new ResponseMetadata<object>()
                {
                    page_number = 1,
                    page_size = data.Count,
                    records = data,
                    total_records_count = 1
                };

                response.success = true;
            }

            return StatusCode(200, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStateById(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(State).Name, eUserPermission.HasViewPermission);

            var response = new ResponseStructure();

            var data = await _service.GetStateByIdAsync(id);
            if (data != null)
            {
                response.data = data;
                response.success = true;
            }

            return StatusCode(200, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateState([FromBody] StateCreateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(State).Name, eUserPermission.HasCreateOrUpdatePermission);

            var response = new ResponseStructure();

            await _service.CreateStateAsync(dto);
            response.data = new { Message = "New State Added Successfully." };
            response.success = true;

            return StatusCode(201, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateState([FromBody] StateUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(State).Name, eUserPermission.HasCreateOrUpdatePermission);

            var response = new ResponseStructure();

            await _service.UpdateStateAsync(dto);
            response.data = new { Message = "State Modified Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteState(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(State).Name, eUserPermission.HasDeletePermission);

            var response = new ResponseStructure();

            await _service.DeleteStateAsync(id);
            response.data = new { Message = $"State with Id = {id} is Deleted Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }
    }
}