using ECommerce.API.Filters;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Shared.RequestModel;
using ECommerce.Shared.ResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
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

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var response = new ResponseStructure();

            var data = await _service.GetUserByIdAsync(id);
            if (data != null)
            {
                response.data = data;
                response.success = true;
                return StatusCode(200, data);
            }

            response.error = $"Requested User for Id = {id} is Not Found.";
            return NotFound(response);
        }

        [HttpPost]
        [ServiceFilter(typeof(ExecutionFilter))]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDTO userDTO)
        {
            var response = new ResponseStructure();

            await _service.CreateUserAsync(userDTO);
            response.data = new { Message = "New User Added Successfully." };
            response.success = true;
            return StatusCode(201, response);
        }

        [HttpPut]
        [ServiceFilter(typeof(ExecutionFilter))]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDTO userDTO)
        {
            var response = new ResponseStructure();

            await _service.UpdateUserAsync(userDTO);
            response.data = new { Message = "User Modified Successfully." };
            response.success = true;
            return StatusCode(200, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var response = new ResponseStructure();

            await _service.DeleteUserAsync(id);
            response.data = new { Message = $"User with Id = {id} is Deleted Successfully." };
            response.success = true;
            return StatusCode(200, response);
        }
    }
}