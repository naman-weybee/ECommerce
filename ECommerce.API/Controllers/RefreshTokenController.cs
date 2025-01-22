using ECommerce.API.Helper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Shared.RequestModel;
using ECommerce.Shared.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Authorize]
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
            var response = new ResponseStructure();

            var isAdmin = User.IsInRole("Admin");

            var userId = (!isAdmin || (isAdmin && isAdminSelf)) ? _userId : default;

            var data = await _service.GetAllRefreshTokensAsync(requestParams, userId);
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRefreshTokenById(Guid id)
        {
            var response = new ResponseStructure();

            var data = await _service.GetRefreshTokenByIdAsync(id);
            if (data != null)
            {
                response.data = data;
                response.success = true;
            }

            return StatusCode(200, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRefreshToken()
        {
            var response = new ResponseStructure();

            var dto = new RefreshTokenCreateDTO() { UserId = _userId };

            var data = await _service.CreateRefreshTokenAsync(dto);
            if (data != null)
            {
                response.data = data;
                response.success = true;
            }

            return StatusCode(201, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRefreshToken([FromBody] RefreshTokenUpdateDTO dto)
        {
            var response = new ResponseStructure();

            dto.UserId = _userId;

            await _service.UpdateRefreshTokenAsync(dto);
            response.data = new { Message = "Refresh Token Modified Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRefreshToken(Guid id)
        {
            var response = new ResponseStructure();

            await _service.DeleteRefreshTokenAsync(id, _userId);
            response.data = new { Message = $"Refresh Token with Id = {id} is Deleted Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }
    }
}