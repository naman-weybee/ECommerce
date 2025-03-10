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

        public OTPController(IOTPService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOTPs([FromQuery] RequestParams requestParams)
        {
            var response = new ResponseStructure();

            var data = await _service.GetAllOTPAsync(requestParams);
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
        public async Task<IActionResult> GetOTPById(Guid id)
        {
            var response = new ResponseStructure();

            var data = await _service.GetOTPByIdAsync(id);
            if (data != null)
            {
                response.data = data;
                response.success = true;
            }

            return StatusCode(200, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOTP([FromBody] OTPCreateFromEmailDTO dto)
        {
            var response = new ResponseStructure();

            await _service.CreateOTPAsync(dto);
            response.data = new { Message = "New OTP Generated Successfully." };
            response.success = true;

            return StatusCode(201, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOTP([FromBody] OTPUpdateDTO dto)
        {
            var response = new ResponseStructure();

            await _service.UpdateOTPAsync(dto);
            response.data = new { Message = "OTP Modified Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpPut("VerifyOTP")]
        public async Task<IActionResult> VerifyOTP([FromBody] OTPVerifyDTO dto)
        {
            var response = new ResponseStructure();

            var data = await _service.VerifyOTPAsync(dto);
            if (data != null)
            {
                response.data = data;
                response.success = true;
            }

            return StatusCode(200, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOTP(Guid id)
        {
            var response = new ResponseStructure();

            await _service.DeleteOTPAsync(id);
            response.data = new { Message = $"OTP with Id = {id} is Deleted Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }
    }
}