using ECommerce.API.Helper.Interfaces;
using ECommerce.Application.DTOs.RolePermission;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Enums;
using ECommerce.Shared.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class RolePermissionController : BaseController
    {
        private readonly IRolePermissionService _service;
        private readonly IControllerHelper _controllerHelper;

        public RolePermissionController(IHTTPHelper httpHelper, IRolePermissionService service, IControllerHelper controllerHelper)
            : base(httpHelper)
        {
            _service = service;
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRolePermissions([FromQuery] RequestParams requestParams)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.RolePermission, eUserPermission.HasFullPermission);

            var data = await _service.GetAllRolePermissionsAsync(requestParams);
            _controllerHelper.SetResponse(_response, data, requestParams);

            return StatusCode(200, _response);
        }

        [HttpGet("GetAllRolePermissionsByRole/{roleId}")]
        public async Task<IActionResult> GetAllRolePermissionsByRole(Guid roleId, [FromQuery] RequestParams requestParams)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.RolePermission, eUserPermission.HasFullPermission);

            var data = await _service.GetAllRolePermissionsByRoleAsync(roleId, requestParams);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(200, _response);
        }

        [HttpGet("GetRolePermissionByIds")]
        public async Task<IActionResult> GetRolePermissionByIds(RolePermissionIDsDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.RolePermission, eUserPermission.HasFullPermission);

            var data = await _service.GetRolePermissionByIdsAsync(dto.RoleId, dto.RoleEntityId);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertRolePermission([FromBody] RolePermissionUpsertDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.RolePermission, eUserPermission.HasFullPermission);

            await _service.UpsertRolePermissionAsync(dto);
            _controllerHelper.SetResponse(_response, "RolePermission Saved Successfully.");

            return StatusCode(200, _response);
        }

        [HttpDelete("DeleteRolePermissionByRole/{roleId}")]
        public async Task<IActionResult> DeleteRolePermissionByRole(Guid roleId)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.RolePermission, eUserPermission.HasFullPermission);

            await _service.DeleteRolePermissionByRoleAsync(roleId);
            _controllerHelper.SetResponse(_response, $"RolePermission with Id = {roleId} is Deleted Successfully.");

            return StatusCode(200, _response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRolePermission(RolePermissionIDsDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.RolePermission, eUserPermission.HasFullPermission);

            await _service.DeleteRolePermissionAsync(dto.RoleId, dto.RoleEntityId);
            _controllerHelper.SetResponse(_response, $"RolePermission with RoleId = {dto.RoleId} and RoleEntityId = {dto.RoleEntityId} is Deleted Successfully.");

            return StatusCode(200, _response);
        }
    }
}