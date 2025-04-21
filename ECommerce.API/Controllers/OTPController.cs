using ECommerce.API.Helper.Interfaces;
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
        private readonly IControllerHelper _controllerHelper;

        internal ResponseStructure _response = new();

        public OTPController(IOTPService service, IControllerHelper controllerHelper)
        {
            _service = service;
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOTPs([FromQuery] RequestParams requestParams)
        {
            var data = await _service.GetAllOTPAsync(requestParams);
            _controllerHelper.SetResponse(_response, data, requestParams);

            return StatusCode(200, _response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOTPById(Guid id)
        {
            var data = await _service.GetOTPByIdAsync(id);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOTP([FromBody] OTPCreateFromEmailDTO dto)
        {
            await _service.CreateOTPAsync(dto);
            _controllerHelper.SetResponse(_response, "New OTP Generated Successfully.");

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOTP([FromBody] OTPUpdateDTO dto)
        {
            await _service.UpdateOTPAsync(dto);
            _controllerHelper.SetResponse(_response, "OTP Modified Successfully.");

            return StatusCode(200, _response);
        }

        [HttpPut("VerifyOTP")]
        public async Task<IActionResult> VerifyOTP([FromBody] OTPVerifyDTO dto)
        {
            var data = await _service.VerifyOTPAsync(dto);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOTP(Guid id)
        {
            await _service.DeleteOTPAsync(id);
            _controllerHelper.SetResponse(_response, $"OTP with Id = {id} is Deleted Successfully.");

            return StatusCode(200, _response);
        }
    }
}