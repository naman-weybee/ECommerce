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
            await _httpHelper.ValidateUserAuthorization(typeof(City).Name, eUserPermission.HasViewPermission);

            var response = new ResponseStructure();

            var data = await _service.GetAllCitiesAsync(requestParams);
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

        [HttpGet("GetAllCitiesByStateId/{stateId}")]
        public async Task<IActionResult> GetAllCitiesByStateId(Guid stateId)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(City).Name, eUserPermission.HasViewPermission);

            var response = new ResponseStructure();

            var data = await _service.GetAllCitiesByStateIdAsync(stateId);
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
        public async Task<IActionResult> GetCityById(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(City).Name, eUserPermission.HasViewPermission);

            var response = new ResponseStructure();

            var data = await _service.GetCityByIdAsync(id);
            if (data != null)
            {
                response.data = data;
                response.success = true;
            }

            return StatusCode(200, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody] CityCreateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(City).Name, eUserPermission.HasCreateOrUpdatePermission);

            var response = new ResponseStructure();

            await _service.CreateCityAsync(dto);
            response.data = new { Message = "New City Added Successfully." };
            response.success = true;

            return StatusCode(201, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCity([FromBody] CityUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(City).Name, eUserPermission.HasCreateOrUpdatePermission);

            var response = new ResponseStructure();

            await _service.UpdateCityAsync(dto);
            response.data = new { Message = "City Modified Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(City).Name, eUserPermission.HasDeletePermission);

            var response = new ResponseStructure();

            await _service.DeleteCityAsync(id);
            response.data = new { Message = $"City with Id = {id} is Deleted Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }
    }
}