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
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service, IHTTPHelper httpHelper)
            : base(httpHelper)
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

            return StatusCode(200, response);
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
            }

            return StatusCode(200, response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDTO dto)
        {
            var response = new ResponseStructure();

            await _service.CreateCategoryAsync(dto);
            response.data = new { Message = "New Category Added Successfully." };
            response.success = true;

            return StatusCode(201, response);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateDTO dto)
        {
            var response = new ResponseStructure();

            await _service.UpdateCategoryAsync(dto);
            response.data = new { Message = "Category Modified Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpPut("AddSubCategory/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddSubCategory(Guid id, [FromBody] CategoryCreateDTO dto)
        {
            var response = new ResponseStructure();

            await _service.AddSubCategoryAsync(id, dto);
            response.data = new { Message = "SubCategory Added Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpPut("RemoveSubCategory/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveSubCategory(Guid id)
        {
            var response = new ResponseStructure();

            await _service.RemoveSubCategoryAsync(id);
            response.data = new { Message = "SubCategory Removed Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
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