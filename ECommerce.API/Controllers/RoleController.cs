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
            await _httpHelper.ValidateUserAuthorization(typeof(Role).Name, eUserPermission.HasViewPermission);

            var data = await _service.GetAllRolesAsync(requestParams);
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
        public async Task<IActionResult> GetRoleById(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Role).Name, eUserPermission.HasViewPermission);

            var data = await _service.GetRoleByIdAsync(id);
            if (data != null)
            {
                _response.data = data;
                _response.success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleCreateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Role).Name, eUserPermission.HasCreateOrUpdatePermission);

            await _service.CreateRoleAsync(dto);
            _response.data = new { Message = "New Role Added Successfully." };
            _response.success = true;

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole([FromBody] RoleUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Role).Name, eUserPermission.HasCreateOrUpdatePermission);

            await _service.UpdateRoleAsync(dto);
            _response.data = new { Message = "Role Modified Successfully." };
            _response.success = true;

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Role).Name, eUserPermission.HasDeletePermission);

            await _service.DeleteRoleAsync(id);
            _response.data = new { Message = $"Role with Id = {id} is Deleted Successfully." };
            _response.success = true;

            return StatusCode(200, _response);
        }
    }
}