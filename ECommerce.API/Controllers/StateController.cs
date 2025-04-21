using ECommerce.API.Helper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
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

        [HttpGet("GetAllStatesByCountryId/{countryId}")]
        public async Task<IActionResult> GetAllStatesByCountryId(Guid countryId)
        {
            var data = await _service.GetAllStatesByCountryIdAsync(countryId);
            if (data != null)
            {
                _response.Data = new ResponseMetadata<object>()
                {
                    Page_Number = 1,
                    Page_Size = data.Count,
                    Records = data,
                    Total_Records_Count = 1
                };

                _response.Success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStateById(Guid id)
        {
            var data = await _service.GetStateByIdAsync(id);
            if (data != null)
            {
                _response.Data = data;
                _response.Success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateState([FromBody] StateCreateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.State, eUserPermission.HasCreateOrUpdatePermission);

            await _service.CreateStateAsync(dto);
            _response.Data = new { Message = "New State Added Successfully." };
            _response.Success = true;

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateState([FromBody] StateUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.State, eUserPermission.HasCreateOrUpdatePermission);

            await _service.UpdateStateAsync(dto);
            _response.Data = new { Message = "State Modified Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteState(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.State, eUserPermission.HasDeletePermission);

            await _service.DeleteStateAsync(id);
            _response.Data = new { Message = $"State with Id = {id} is Deleted Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }
    }
}