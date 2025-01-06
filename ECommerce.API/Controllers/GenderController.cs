using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Shared.RequestModel;
using ECommerce.Shared.ResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class GenderController : ControllerBase
    {
        private readonly IGenderService _service;

        public GenderController(IGenderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenders([FromQuery] RequestParams requestParams)
        {
            var response = new ResponseStructure();

            var data = await _service.GetAllGendersAsync(requestParams);
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
        public async Task<IActionResult> GetGenderById(Guid id)
        {
            var response = new ResponseStructure();

            var data = await _service.GetGenderByIdAsync(id);
            if (data != null)
            {
                response.data = data;
                response.success = true;
            }

            return StatusCode(200, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGender([FromBody] GenderCreateDTO dto)
        {
            var response = new ResponseStructure();

            await _service.CreateGenderAsync(dto);
            response.data = new { Message = "New Gender Added Successfully." };
            response.success = true;

            return StatusCode(201, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGender([FromBody] GenderUpdateDTO dto)
        {
            var response = new ResponseStructure();

            await _service.UpdateGenderAsync(dto);
            response.data = new { Message = "Gender Modified Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGender(Guid id)
        {
            var response = new ResponseStructure();

            await _service.DeleteGenderAsync(id);
            response.data = new { Message = $"Gender with Id = {id} is Deleted Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }
    }
}