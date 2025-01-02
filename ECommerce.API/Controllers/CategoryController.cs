using ECommerce.API.Filters;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Shared.RequestModel;
using ECommerce.Shared.ResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] RequestParams requestParams)
        {
            var response = new ResponseStructure();

            var data = await _service.GetAllCategoriesAsync(requestParams);
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

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var response = new ResponseStructure();

            var data = await _service.GetCategoryByIdAsync(id);
            if (data != null)
            {
                response.data = data;
                response.success = true;
                return StatusCode(200, data);
            }

            response.error = $"Requested Category for Id = {id} is Not Found.";

            return NotFound(response);
        }

        [HttpPost]
        [ServiceFilter(typeof(ExecutionFilter))]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDTO dto)
        {
            var response = new ResponseStructure();

            await _service.CreateCategoryAsync(dto);
            response.data = new { Message = "New Category Added Successfully." };
            response.success = true;

            return StatusCode(201, response);
        }

        [HttpPut]
        [ServiceFilter(typeof(ExecutionFilter))]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateDTO dto)
        {
            var response = new ResponseStructure();

            await _service.UpdateCategoryAsync(dto);
            response.data = new { Message = "Category Modified Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpPut("AddSubCategory/{id}")]
        [ServiceFilter(typeof(ExecutionFilter))]
        public async Task<IActionResult> AddSubCategory(Guid id, [FromBody] CategoryCreateDTO dto)
        {
            var response = new ResponseStructure();

            await _service.AddSubCategoryAsync(id, dto);
            response.data = new { Message = "SubCategory Added Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpPut("RemoveSubCategory/{id}")]
        [ServiceFilter(typeof(ExecutionFilter))]
        public async Task<IActionResult> RemoveSubCategory(Guid id)
        {
            var response = new ResponseStructure();

            await _service.RemoveSubCategoryAsync(id);
            response.data = new { Message = "SubCategory Removed Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var response = new ResponseStructure();

            await _service.DeleteCategoryAsync(id);
            response.data = new { Message = $"Category with Id = {id} is Deleted Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }
    }
}