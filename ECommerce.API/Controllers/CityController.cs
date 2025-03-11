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

        [HttpGet("GetAllCitiesByStateId/{stateId}")]
        public async Task<IActionResult> GetAllCitiesByStateId(Guid stateId)
        {
            var data = await _service.GetAllCitiesByStateIdAsync(stateId);
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
        public async Task<IActionResult> GetCityById(Guid id)
        {
            var data = await _service.GetCityByIdAsync(id);
            if (data != null)
            {
                _response.data = data;
                _response.success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody] CityCreateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.City, eUserPermission.HasCreateOrUpdatePermission);

            await _service.CreateCityAsync(dto);
            _response.data = new { Message = "New City Added Successfully." };
            _response.success = true;

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCity([FromBody] CityUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.City, eUserPermission.HasCreateOrUpdatePermission);

            await _service.UpdateCityAsync(dto);
            _response.data = new { Message = "City Modified Successfully." };
            _response.success = true;

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.City, eUserPermission.HasDeletePermission);

            await _service.DeleteCityAsync(id);
            _response.data = new { Message = $"City with Id = {id} is Deleted Successfully." };
            _response.success = true;

            return StatusCode(200, _response);
        }
    }
}