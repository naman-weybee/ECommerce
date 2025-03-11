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
    public class UserController : BaseController
    {
        private readonly IUserService _service;

        public UserController(IUserService service, IHTTPHelper httpHelper)
            : base(httpHelper)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] RequestParams requestParams)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(User).Name, eUserPermission.HasViewPermission);

            var data = await _service.GetAllUsersAsync(requestParams);
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

        [HttpGet("GetCurrentUser")]
        public async Task<IActionResult> GetUserById()
        {
            await _httpHelper.ValidateUserAuthorization(typeof(User).Name, eUserPermission.HasViewPermission);

            var data = await _service.GetUserByIdAsync(_userId);
            if (data != null)
            {
                _response.data = data;
                _response.success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDTO dto)
        {
            await _service.CreateUserAsync(dto);
            _response.data = new { Message = "User Created Successfully." };
            _response.success = true;

            return StatusCode(201, _response);
        }

        [HttpPost("PasswordReset")]
        public async Task<IActionResult> PasswordReset([FromBody] PasswordResetDTO dto)
        {
            await _service.PasswordResetAsync(dto);
            _response.data = new { Message = "Password Reset Successfully." };
            _response.success = true;

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDTO dto)
        {
            dto.Id = _userId;

            await _service.UpdateUserAsync(dto);
            _response.data = new { Message = "User Modified Successfully." };
            _response.success = true;

            return StatusCode(200, _response);
        }

        [HttpDelete("DeleteCurrentUser")]
        public async Task<IActionResult> DeleteUser()
        {
            await _httpHelper.ValidateUserAuthorization(typeof(User).Name, eUserPermission.HasDeletePermission);

            await _service.DeleteUserAsync(_userId);
            _response.data = new { Message = $"User with Id = {_userId} is Deleted Successfully." };
            _response.success = true;

            return StatusCode(200, _response);
        }
    }
}