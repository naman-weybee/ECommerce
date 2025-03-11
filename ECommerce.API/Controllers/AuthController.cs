using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Shared.ResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _service;

        internal ResponseStructure _response = new();

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserCreateDTO dto)
        {
            await _service.RegisterAsync(dto);
            _response.data = new { Message = "Verification email sent." };
            _response.success = true;

            return StatusCode(201, _response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO dto)
        {
            var data = await _service.LoginAsync(dto);
            if (data != null)
            {
                _response.data = data;
                _response.success = true;
            }

            return StatusCode(201, _response);
        }

        [HttpPost("RevokeRefreshToken")]
        public async Task<IActionResult> RevokeRefreshToken([FromBody] RevokeRefreshTokenDTO dto)
        {
            await _service.RevokeRefreshTokenAsync(dto);
            _response.data = new { Message = "Refresh Token Revoked Successfully." };
            _response.success = true;

            return StatusCode(201, _response);
        }

        [HttpPost("ReCreateAccessToken")]
        public async Task<IActionResult> ReCreateAccessToken([FromBody] AccessTokenCreateDTO dto)
        {
            var data = await _service.ReCreateAccessTokenAsync(dto);
            if (data != null)
            {
                _response.data = data;
                _response.success = true;
            }

            return StatusCode(201, _response);
        }

        [HttpGet("VerifyEmail")]
        public async Task<IActionResult> VerifyEmail(string token)
        {
            await _service.VerifyEmailAsync(token);
            _response.data = new { Message = "User verified Successfully." };
            _response.success = true;

            return StatusCode(200, _response);
        }
    }
}