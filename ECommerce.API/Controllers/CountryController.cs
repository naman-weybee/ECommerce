using ECommerce.API.Helper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountryById(Guid id)
        {
            var data = await _service.GetCountryByIdAsync(id);
            if (data != null)
            {
                _response.Data = data;
                _response.Success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCountry([FromBody] CountryCreateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Country, eUserPermission.HasCreateOrUpdatePermission);

            await _service.CreateCountryAsync(dto);
            _response.Data = new { Message = "New Country Added Successfully." };
            _response.Success = true;

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCountry([FromBody] CountryUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Country, eUserPermission.HasCreateOrUpdatePermission);

            await _service.UpdateCountryAsync(dto);
            _response.Data = new { Message = "Country Modified Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Country, eUserPermission.HasDeletePermission);

            await _service.DeleteCountryAsync(id);
            _response.Data = new { Message = $"Country with Id = {id} is Deleted Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }
    }
}