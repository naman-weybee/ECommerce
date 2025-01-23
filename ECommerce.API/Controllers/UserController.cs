using ECommerce.API.Helper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Shared.RequestModel;
using ECommerce.Shared.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Authorize(Roles = "Admin")]
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
            var response = new ResponseStructure();

            var data = await _service.GetAllUsersAsync(requestParams);
            if (data != null)
            {
                response.data = new ResponseMetadata<object>()
                {
                    page_number = requestParams.pageNumber,
                    page_size = requestParams.pageSize,
                    records = data,
                    total_records_count = requestParams.recordCount
                };

                response.success = true;
            }

            return StatusCode(200, response);
        }

        [HttpGet("GetCurrentUser")]
        public async Task<IActionResult> GetUserById()
        {
            var response = new ResponseStructure();

            var data = await _service.GetUserByIdAsync(_userId);
            if (data != null)
            {
                response.data = data;
                response.success = true;
            }

            return StatusCode(200, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDTO dto)
        {
            var response = new ResponseStructure();

            await _service.CreateUserAsync(dto);
            response.data = new { Message = "User Created Successfully." };
            response.success = true;

            return StatusCode(201, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDTO dto)
        {
            var response = new ResponseStructure();

            dto.Id = _userId;

            await _service.UpdateUserAsync(dto);
            response.data = new { Message = "User Modified Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpDelete("DeleteCurrentUser")]
        public async Task<IActionResult> DeleteUser()
        {
            var response = new ResponseStructure();

            await _service.DeleteUserAsync(_userId);
            response.data = new { Message = $"User with Id = {_userId} is Deleted Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }
    }
}