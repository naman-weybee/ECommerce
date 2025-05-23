using ECommerce.API.Helper.Interfaces;
using ECommerce.Application.DTOs.Role;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Enums;
using ECommerce.Shared.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class RoleController : BaseController
    {
        private readonly IRoleService _service;
        private readonly IControllerHelper _controllerHelper;

        public RoleController(IHTTPHelper httpHelper, IRoleService service, IControllerHelper controllerHelper)
            : base(httpHelper)
        {
            _service = service;
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles([FromQuery] RequestParams requestParams)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Role, eUserPermission.HasViewPermission);

            var data = await _service.GetAllRolesAsync(requestParams, true);
            _controllerHelper.SetResponse(_response, data, requestParams);

            return StatusCode(200, _response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Role, eUserPermission.HasViewPermission);

            var data = await _service.GetRoleByIdAsync(id, true);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertRole([FromBody] RoleUpsertDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Role, eUserPermission.HasCreateOrUpdatePermission);

            await _service.UpsertRoleAsync(dto);
            _controllerHelper.SetResponse(_response, "Role Saved Successfully.");

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Role, eUserPermission.HasDeletePermission);

            await _service.DeleteRoleAsync(id);
            _controllerHelper.SetResponse(_response, $"Role with Id = {id} is Deleted Successfully.");

            return StatusCode(200, _response);
        }
    }
}