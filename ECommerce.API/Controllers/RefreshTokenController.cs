using ECommerce.API.Helper.Interfaces;
using ECommerce.Application.DTOs;
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

        [HttpGet("GetRefreshTokensForUser")]
        public async Task<IActionResult> GetRefreshTokensForUser([FromQuery] RequestParams requestParams)
        {
            var data = await _service.GetAllRefreshTokensAsync(requestParams, _userId);
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

        [HttpPost]
        public async Task<IActionResult> CreateRefreshToken()
        {
            var dto = new RefreshTokenCreateDTO() { UserId = _userId };

            var data = await _service.CreateRefreshTokenAsync(dto);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRefreshToken([FromBody] RefreshTokenUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.RefreshToken, eUserPermission.HasCreateOrUpdatePermission);

            dto.UserId = _userId;

            await _service.UpdateRefreshTokenAsync(dto);
            _controllerHelper.SetResponse(_response, "Refresh Token Modified Successfully.");

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