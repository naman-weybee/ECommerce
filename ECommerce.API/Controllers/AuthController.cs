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
            _response.Data = new { Message = "Verification email sent." };
            _response.Success = true;

            return StatusCode(201, _response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO dto)
        {
            var data = await _service.LoginAsync(dto);
            if (data != null)
            {
                _response.Data = data;
                _response.Success = true;
            }

            return StatusCode(201, _response);
        }

        [HttpPost("RevokeRefreshToken")]
        public async Task<IActionResult> RevokeRefreshToken([FromBody] RevokeRefreshTokenDTO dto)
        {
            await _service.RevokeRefreshTokenAsync(dto);
            _response.Data = new { Message = "Refresh Token Revoked Successfully." };
            _response.Success = true;

            return StatusCode(201, _response);
        }

        [HttpPost("ReCreateAccessToken")]
        public async Task<IActionResult> ReCreateAccessToken([FromBody] AccessTokenCreateDTO dto)
        {
            var data = await _service.ReCreateAccessTokenAsync(dto);
            if (data != null)
            {
                _response.Data = data;
                _response.Success = true;
            }

            return StatusCode(201, _response);
        }

        [HttpGet("VerifyEmail")]
        public async Task<IActionResult> VerifyEmail(string token)
        {
            await _service.VerifyEmailAsync(token);
            _response.Data = new { Message = "User verified Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }
    }
}