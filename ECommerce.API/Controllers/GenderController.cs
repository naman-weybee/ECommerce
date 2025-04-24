using ECommerce.API.Helper.Interfaces;
using ECommerce.Application.DTOs.Gender;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Enums;
using ECommerce.Shared.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class GenderController : BaseController
    {
        private readonly IGenderService _service;
        private readonly IControllerHelper _controllerHelper;

        public GenderController(IHTTPHelper httpHelper, IGenderService service, IControllerHelper controllerHelper)
            : base(httpHelper)
        {
            _service = service;
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenders([FromQuery] RequestParams requestParams)
        {
            var data = await _service.GetAllGendersAsync(requestParams);
            _controllerHelper.SetResponse(_response, data, requestParams);

            return StatusCode(200, _response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenderById(Guid id)
        {
            var data = await _service.GetGenderByIdAsync(id);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertGender([FromBody] GenderUpsertDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Gender, eUserPermission.HasCreateOrUpdatePermission);

            await _service.UpsertGenderAsync(dto);
            _controllerHelper.SetResponse(_response, "Gender Saved Successfully.");

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGender(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Gender, eUserPermission.HasDeletePermission);

            await _service.DeleteGenderAsync(id);
            _controllerHelper.SetResponse(_response, $"Gender with Id = {id} is Deleted Successfully.");

            return StatusCode(200, _response);
        }
    }
}