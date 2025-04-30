using ECommerce.API.Helper.Interfaces;
using ECommerce.Application.DTOs.Auth;
using ECommerce.Application.DTOs.RefreshToken;
using ECommerce.Application.DTOs.User;
using ECommerce.Application.Interfaces;
using ECommerce.Shared.ResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _service;
        private readonly IControllerHelper _controllerHelper;

        internal ResponseStructure _response = new();

        public AuthController(IAuthService service, IControllerHelper controllerHelper)
        {
            _service = service;
            _controllerHelper = controllerHelper;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserUpsertDTO dto)
        {
            await _service.RegisterAsync(dto);
            _controllerHelper.SetResponse(_response, "Verification email sent.");

            return StatusCode(200, _response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO dto)
        {
            var data = await _service.LoginAsync(dto);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(200, _response);
        }

        [HttpPost("RevokeRefreshToken")]
        public async Task<IActionResult> RevokeRefreshToken([FromBody] RevokeRefreshTokenDTO dto)
        {
            await _service.RevokeRefreshTokenAsync(dto);
            _controllerHelper.SetResponse(_response, "Refresh Token Revoked Successfully.");

            return StatusCode(200, _response);
        }

        [HttpPost("ReCreateAccessToken")]
        public async Task<IActionResult> ReCreateAccessToken([FromBody] AccessTokenCreateDTO dto)
        {
            var data = await _service.ReCreateAccessTokenAsync(dto);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(200, _response);
        }

        [HttpPut("ReSendEmailVerification")]
        public async Task<IActionResult> ReSendEmailVerification([FromBody] ResendEmailVerificationDTO dto)
        {
            await _service.ReSendEmailVerificationAsync(dto);
            _controllerHelper.SetResponse(_response, "Verification Email ReSent Successfully.");

            return StatusCode(200, _response);
        }

        [HttpPut("VerifyEmail")]
        public async Task<IActionResult> VerifyEmail(string token)
        {
            await _service.VerifyEmailAsync(token);
            _controllerHelper.SetResponse(_response, "User verified Successfully.");

            return StatusCode(200, _response);
        }
    }
}