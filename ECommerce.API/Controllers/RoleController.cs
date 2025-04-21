using ECommerce.API.Helper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Enums;
using ECommerce.Shared.RequestModel;
using ECommerce.Shared.ResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class RoleController : BaseController
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service, IHTTPHelper httpHelper)
            : base(httpHelper)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles([FromQuery] RequestParams requestParams)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Role, eUserPermission.HasViewPermission);

            var data = await _service.GetAllRolesAsync(requestParams);
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
        public async Task<IActionResult> GetRoleById(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Role, eUserPermission.HasViewPermission);

            var data = await _service.GetRoleByIdAsync(id);
            if (data != null)
            {
                _response.Data = data;
                _response.Success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleCreateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Role, eUserPermission.HasCreateOrUpdatePermission);

            await _service.CreateRoleAsync(dto);
            _response.Data = new { Message = "New Role Added Successfully." };
            _response.Success = true;

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole([FromBody] RoleUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Role, eUserPermission.HasCreateOrUpdatePermission);

            await _service.UpdateRoleAsync(dto);
            _response.Data = new { Message = "Role Modified Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Role, eUserPermission.HasDeletePermission);

            await _service.DeleteRoleAsync(id);
            _response.Data = new { Message = $"Role with Id = {id} is Deleted Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }
    }
}