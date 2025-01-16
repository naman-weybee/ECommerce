using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Shared.ResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserCreateDTO dto)
        {
            var response = new ResponseStructure();

            var data = await _service.RegisterAsync(dto);
            if (data != null)
            {
                response.data = data;
                response.success = true;
            }

            return StatusCode(201, response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO dto)
        {
            var response = new ResponseStructure();

            var data = await _service.LoginAsync(dto);
            if (data != null)
            {
                response.data = data;
                response.success = true;
            }

            return StatusCode(201, response);
        }

        [HttpPost("RevokeRefreshToken")]
        public async Task<IActionResult> RevokeRefreshToken([FromBody] RevokeRefreshTokenDTO dto)
        {
            var response = new ResponseStructure();

            await _service.RevokeRefreshTokenAsync(dto);
            response.data = new { Message = "Refresh Token Revoked Successfully." };
            response.success = true;

            return StatusCode(201, response);
        }

        [HttpPost("ReCreateAccessToken")]
        public async Task<IActionResult> ReCreateAccessToken([FromBody] AccessTokenCreateDTO dto)
        {
            var response = new ResponseStructure();

            var data = await _service.ReCreateAccessTokenAsync(dto);
            if (data != null)
            {
                response.data = data;
                response.success = true;
            }

            return StatusCode(201, response);
        }
    }
}