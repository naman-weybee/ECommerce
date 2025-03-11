using ECommerce.API.Helper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Enums;
using ECommerce.Shared.RequestModel;
using ECommerce.Shared.ResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class RefreshTokenController : BaseController
    {
        private readonly IRefreshTokenService _service;

        public RefreshTokenController(IRefreshTokenService service, IHTTPHelper httpHelper)
            : base(httpHelper)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRefreshTokens([FromQuery] RequestParams requestParams, [FromQuery] bool isAdminSelf = false)
        {
            var isAdmin = User.IsInRole("Admin");
            var userId = (!isAdmin || (isAdmin && isAdminSelf)) ? _userId : default;

            var data = await _service.GetAllRefreshTokensAsync(requestParams, userId);
            if (data != null)
            {
                _response.data = new ResponseMetadata<object>()
                {
                    page_number = requestParams.pageNumber,
                    page_size = requestParams.pageSize,
                    records = data,
                    total_records_count = requestParams.recordCount
                };

                _response.success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRefreshTokenById(Guid id)
        {
            var data = await _service.GetRefreshTokenByIdAsync(id);
            if (data != null)
            {
                _response.data = data;
                _response.success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRefreshToken()
        {
            var dto = new RefreshTokenCreateDTO() { UserId = _userId };

            var data = await _service.CreateRefreshTokenAsync(dto);
            if (data != null)
            {
                _response.data = data;
                _response.success = true;
            }

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRefreshToken([FromBody] RefreshTokenUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.RefreshToken, eUserPermission.HasCreateOrUpdatePermission);

            dto.UserId = _userId;

            await _service.UpdateRefreshTokenAsync(dto);
            _response.data = new { Message = "Refresh Token Modified Successfully." };
            _response.success = true;

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRefreshToken(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.RefreshToken, eUserPermission.HasDeletePermission);

            await _service.DeleteRefreshTokenAsync(id, _userId);
            _response.data = new { Message = $"Refresh Token with Id = {id} is Deleted Successfully." };
            _response.success = true;

            return StatusCode(200, _response);
        }
    }
}