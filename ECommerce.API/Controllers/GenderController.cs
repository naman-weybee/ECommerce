using ECommerce.API.Helper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
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
        public async Task<IActionResult> GetGenderById(Guid id)
        {
            var data = await _service.GetGenderByIdAsync(id);
            if (data != null)
            {
                _response.Data = data;
                _response.Success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGender([FromBody] GenderCreateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Gender, eUserPermission.HasCreateOrUpdatePermission);

            await _service.CreateGenderAsync(dto);
            _response.Data = new { Message = "New Gender Added Successfully." };
            _response.Success = true;

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGender([FromBody] GenderUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Gender, eUserPermission.HasCreateOrUpdatePermission);

            await _service.UpdateGenderAsync(dto);
            _response.Data = new { Message = "Gender Modified Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGender(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Gender, eUserPermission.HasDeletePermission);

            await _service.DeleteGenderAsync(id);
            _response.Data = new { Message = $"Gender with Id = {id} is Deleted Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }
    }
}