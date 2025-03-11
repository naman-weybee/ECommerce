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
    public class GenderController : BaseController
    {
        private readonly IGenderService _service;

        public GenderController(IGenderService service, IHTTPHelper httpHelper)
            : base(httpHelper)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenders([FromQuery] RequestParams requestParams)
        {
            var data = await _service.GetAllGendersAsync(requestParams);
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
        public async Task<IActionResult> GetGenderById(Guid id)
        {
            var data = await _service.GetGenderByIdAsync(id);
            if (data != null)
            {
                _response.data = data;
                _response.success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGender([FromBody] GenderCreateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Gender).Name, eUserPermission.HasCreateOrUpdatePermission);

            await _service.CreateGenderAsync(dto);
            _response.data = new { Message = "New Gender Added Successfully." };
            _response.success = true;

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGender([FromBody] GenderUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Gender).Name, eUserPermission.HasCreateOrUpdatePermission);

            await _service.UpdateGenderAsync(dto);
            _response.data = new { Message = "Gender Modified Successfully." };
            _response.success = true;

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGender(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Gender).Name, eUserPermission.HasDeletePermission);

            await _service.DeleteGenderAsync(id);
            _response.data = new { Message = $"Gender with Id = {id} is Deleted Successfully." };
            _response.success = true;

            return StatusCode(200, _response);
        }
    }
}