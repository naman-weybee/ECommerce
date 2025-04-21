using ECommerce.API.Helper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Enums;
using ECommerce.Shared.RequestModel;
using ECommerce.Shared.ResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class CityController : BaseController
    {
        private readonly ICityService _service;

        public CityController(ICityService service, IHTTPHelper httpHelper)
            : base(httpHelper)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCities([FromQuery] RequestParams requestParams)
        {
            var data = await _service.GetAllCitiesAsync(requestParams);
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

        [HttpGet("GetAllCitiesByStateId/{stateId}")]
        public async Task<IActionResult> GetAllCitiesByStateId(Guid stateId)
        {
            var data = await _service.GetAllCitiesByStateIdAsync(stateId);
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
        public async Task<IActionResult> GetCityById(Guid id)
        {
            var data = await _service.GetCityByIdAsync(id);
            if (data != null)
            {
                _response.Data = data;
                _response.Success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody] CityCreateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.City, eUserPermission.HasCreateOrUpdatePermission);

            await _service.CreateCityAsync(dto);
            _response.Data = new { Message = "New City Added Successfully." };
            _response.Success = true;

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCity([FromBody] CityUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.City, eUserPermission.HasCreateOrUpdatePermission);

            await _service.UpdateCityAsync(dto);
            _response.Data = new { Message = "City Modified Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.City, eUserPermission.HasDeletePermission);

            await _service.DeleteCityAsync(id);
            _response.Data = new { Message = $"City with Id = {id} is Deleted Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }
    }
}