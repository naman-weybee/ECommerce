using ECommerce.API.Helper.Interfaces;
using ECommerce.Application.DTOs.RefreshToken;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Enums;
using ECommerce.Shared.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class RefreshTokenController : BaseController
    {
        private readonly IRefreshTokenService _service;
        private readonly IControllerHelper _controllerHelper;

        public RefreshTokenController(IHTTPHelper httpHelper, IRefreshTokenService service, IControllerHelper controllerHelper)
            : base(httpHelper)
        {
            _service = service;
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRefreshTokens([FromQuery] RequestParams requestParams)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.RefreshToken, eUserPermission.HasFullPermission);

            var data = await _service.GetAllRefreshTokensAsync(requestParams);
            _controllerHelper.SetResponse(_response, data, requestParams);

            return StatusCode(200, _response);
        }

        [HttpGet("GetAllRefreshTokensByUser")]
        public async Task<IActionResult> GetAllRefreshTokensByUser([FromQuery] RequestParams requestParams)
        {
            var data = await _service.GetAllRefreshTokensByUserAsync(_userId, requestParams);
            _controllerHelper.SetResponse(_response, data, requestParams);

            return StatusCode(200, _response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRefreshTokenById(Guid id)
        {
            var data = await _service.GetRefreshTokenByIdAsync(id);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(200, _response);
        }

        [HttpGet("GetUserSpecificRefreshTokenById/{id}/{userId}")]
        public async Task<IActionResult> GetUserSpecificRefreshTokenById(Guid id, Guid userId)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.RefreshToken, eUserPermission.HasFullPermission);

            var data = await _service.GetUserSpecificRefreshTokenByIdAsync(id, userId);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertRefreshToken()
        {
            var dto = new RefreshTokenUpsertDTO() { UserId = _userId };

            var data = await _service.UpsertRefreshTokenAsync(dto);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRefreshToken(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.RefreshToken, eUserPermission.HasDeletePermission);

            await _service.DeleteRefreshTokenAsync(id, _userId);
            _controllerHelper.SetResponse(_response, $"Refresh Token with Id = {id} is Deleted Successfully.");

            return StatusCode(200, _response);
        }
    }
}