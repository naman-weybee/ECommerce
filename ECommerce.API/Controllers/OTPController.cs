using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Shared.RequestModel;
using ECommerce.Shared.ResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class OTPController : Controller
    {
        private readonly IOTPService _service;

        internal ResponseStructure _response = new();

        public OTPController(IOTPService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOTPs([FromQuery] RequestParams requestParams)
        {
            var data = await _service.GetAllOTPAsync(requestParams);
            if (data != null)
            {
                _response.Data = new ResponseMetadata<object>()
                {
                    Page_Number = requestParams.PageNumber,
                    Page_Size = requestParams.PageSize,
                    Records = data,
                    Total_Records_Count = requestParams.RecordCount
                };

                _response.Success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOTPById(Guid id)
        {
            var data = await _service.GetOTPByIdAsync(id);
            if (data != null)
            {
                _response.Data = data;
                _response.Success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOTP([FromBody] OTPCreateFromEmailDTO dto)
        {
            await _service.CreateOTPAsync(dto);
            _response.Data = new { Message = "New OTP Generated Successfully." };
            _response.Success = true;

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOTP([FromBody] OTPUpdateDTO dto)
        {
            await _service.UpdateOTPAsync(dto);
            _response.Data = new { Message = "OTP Modified Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }

        [HttpPut("VerifyOTP")]
        public async Task<IActionResult> VerifyOTP([FromBody] OTPVerifyDTO dto)
        {
            var data = await _service.VerifyOTPAsync(dto);
            if (data != null)
            {
                _response.Data = data;
                _response.Success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOTP(Guid id)
        {
            await _service.DeleteOTPAsync(id);
            _response.Data = new { Message = $"OTP with Id = {id} is Deleted Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }
    }
}