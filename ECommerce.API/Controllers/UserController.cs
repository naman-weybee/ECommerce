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
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
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

            var userId = HTTPHelper.GetUserId();

            var data = await _service.GetUserByIdAsync(userId);
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

            dto.Id = HTTPHelper.GetUserId();

            await _service.UpdateUserAsync(dto);
            response.data = new { Message = "User Modified Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpDelete("DeleteCurrentUser")]
        public async Task<IActionResult> DeleteUser()
        {
            var response = new ResponseStructure();

            var userId = HTTPHelper.GetUserId();

            await _service.DeleteUserAsync(userId);
            response.data = new { Message = $"User with Id = {userId} is Deleted Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [AllowAnonymous]
        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail(string token)
        {
            var response = new ResponseStructure();

            await _service.VerifyEmailAsync(token);
            response.data = new { Message = "User verified Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }
    }
}