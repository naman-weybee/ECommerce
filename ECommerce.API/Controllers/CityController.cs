using ECommerce.API.Helper.Interfaces;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Enums;
using ECommerce.Shared.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class CityController : BaseController
    {
        private readonly ICityService _service;
        private readonly IControllerHelper _controllerHelper;

        public CityController(IHTTPHelper httpHelper, ICityService service, IControllerHelper controllerHelper)
            : base(httpHelper)
        {
            _service = service;
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCities([FromQuery] RequestParams requestParams)
        {
            var data = await _service.GetAllCitiesAsync(requestParams);
            _controllerHelper.SetResponse(_response, data, requestParams);

            return StatusCode(200, _response);
        }

        [HttpGet("GetAllCitiesByStateId/{stateId}")]
        public async Task<IActionResult> GetAllCitiesByStateId(Guid stateId, [FromQuery] RequestParams requestParams)
        {
            var data = await _service.GetAllCitiesByStateIdAsync(stateId, requestParams);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(200, _response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCityById(Guid id)
        {
            var data = await _service.GetCityByIdAsync(id);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody] CityCreateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.City, eUserPermission.HasCreateOrUpdatePermission);

            await _service.CreateCityAsync(dto);
            _controllerHelper.SetResponse(_response, "New City Added Successfully.");

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCity([FromBody] CityUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.City, eUserPermission.HasCreateOrUpdatePermission);

            await _service.UpdateCityAsync(dto);
            _controllerHelper.SetResponse(_response, "City Modified Successfully.");

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.City, eUserPermission.HasDeletePermission);

            await _service.DeleteCityAsync(id);
            _controllerHelper.SetResponse(_response, $"City with Id = {id} is Deleted Successfully.");

            return StatusCode(200, _response);
        }
    }
}