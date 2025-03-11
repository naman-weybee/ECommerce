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
    public class CountryController : BaseController
    {
        private readonly ICountryService _service;

        public CountryController(ICountryService service, IHTTPHelper httpHelper)
            : base(httpHelper)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries([FromQuery] RequestParams requestParams)
        {
            var data = await _service.GetAllCountriesAsync(requestParams);
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountryById(Guid id)
        {
            var data = await _service.GetCountryByIdAsync(id);
            if (data != null)
            {
                _response.data = data;
                _response.success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCountry([FromBody] CountryCreateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Country).Name, eUserPermission.HasCreateOrUpdatePermission);

            await _service.CreateCountryAsync(dto);
            _response.data = new { Message = "New Country Added Successfully." };
            _response.success = true;

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCountry([FromBody] CountryUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Country).Name, eUserPermission.HasCreateOrUpdatePermission);

            await _service.UpdateCountryAsync(dto);
            _response.data = new { Message = "Country Modified Successfully." };
            _response.success = true;

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Country).Name, eUserPermission.HasDeletePermission);

            await _service.DeleteCountryAsync(id);
            _response.data = new { Message = $"Country with Id = {id} is Deleted Successfully." };
            _response.success = true;

            return StatusCode(200, _response);
        }
    }
}