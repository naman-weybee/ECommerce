using ECommerce.API.Attributes;
using ECommerce.API.Helper.Interfaces;
using ECommerce.Application.DTOs.User;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Enums;
using ECommerce.Shared.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _service;
        private readonly IControllerHelper _controllerHelper;

        public UserController(IHTTPHelper httpHelper, IUserService service, IControllerHelper controllerHelper)
            : base(httpHelper)
        {
            _service = service;
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] RequestParams requestParams)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.User, eUserPermission.HasFullPermission);

            var data = await _service.GetAllUsersAsync(requestParams, true);
            _controllerHelper.SetResponse(_response, data, requestParams);

            return StatusCode(200, _response);
        }

        [HttpGet("GetAllActiveUsers")]
        public async Task<IActionResult> GetAllActiveUsers([FromQuery] RequestParams requestParams)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.User, eUserPermission.HasFullPermission);

            var data = await _service.GetAllActiveUsersAsync(requestParams, true);
            _controllerHelper.SetResponse(_response, data, requestParams);

            return StatusCode(200, _response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.User, eUserPermission.HasFullPermission);

            var data = await _service.GetUserByIdAsync(id, true);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(200, _response);
        }

        [HttpGet("GetCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.User, eUserPermission.HasViewPermission);

            var data = await _service.GetUserByIdAsync(_userId, true);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(200, _response);
        }

        [BypassDbTransection]
        [HttpPost]
        public async Task<IActionResult> UpsertUser([FromBody] UserUpsertDTO dto)
        {
            await _service.UpsertUserAsync(dto);
            _controllerHelper.SetResponse(_response, "User Saved Successfully.");

            return StatusCode(200, _response);
        }

        [HttpGet("ReSendEmailVerification")]
        public async Task<IActionResult> ReSendEmailVerification()
        {
            await _service.ReSendEmailVerificationAsync(_userId);
            _controllerHelper.SetResponse(_response, "Verification Email ReSend Successfully.");

            return StatusCode(200, _response);
        }

        [BypassDbTransection]
        [HttpPost("PasswordReset")]
        public async Task<IActionResult> PasswordReset([FromBody] PasswordResetDTO dto)
        {
            await _service.PasswordResetAsync(dto);
            _controllerHelper.SetResponse(_response, "Password Reset Successfully.");

            return StatusCode(200, _response);
        }

        [HttpDelete("DeleteCurrentUser")]
        public async Task<IActionResult> DeleteUser()
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.User, eUserPermission.HasDeletePermission);

            await _service.DeleteUserAsync(_userId);
            _controllerHelper.SetResponse(_response, $"User with Id = {_userId} is Deleted Successfully.");

            return StatusCode(200, _response);
        }
    }
}