using ECommerce.API.Helper.Interfaces;
using ECommerce.Application.DTOs.Country;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Enums;
using ECommerce.Shared.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class CountryController : BaseController
    {
        private readonly ICountryService _service;
        private readonly IControllerHelper _controllerHelper;

        public CountryController(IHTTPHelper httpHelper, ICountryService service, IControllerHelper controllerHelper)
            : base(httpHelper)
        {
            _service = service;
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries([FromQuery] RequestParams requestParams)
        {
            var data = await _service.GetAllCountriesAsync(requestParams, true);
            _controllerHelper.SetResponse(_response, data, requestParams);

            return StatusCode(200, _response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountryById(Guid id)
        {
            var data = await _service.GetCountryByIdAsync(id, true);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertCountry([FromBody] CountryUpsertDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Country, eUserPermission.HasCreateOrUpdatePermission);

            await _service.UpsertCountryAsync(dto);
            _controllerHelper.SetResponse(_response, "Country Saved Successfully.");

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Country, eUserPermission.HasDeletePermission);

            await _service.DeleteCountryAsync(id);
            _controllerHelper.SetResponse(_response, $"Country with Id = {id} is Deleted Successfully.");

            return StatusCode(200, _response);
        }
    }
}