using ECommerce.API.Helper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Shared.RequestModel;
using ECommerce.Shared.ResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
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
            await _httpHelper.ValidateUserAuthorization(typeof(Category).Name, eUserPermission.HasViewPermission);

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
            await _httpHelper.ValidateUserAuthorization(typeof(Category).Name, eUserPermission.HasViewPermission);

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
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Category).Name, eUserPermission.HasCreateOrUpdatePermission);

            var response = new ResponseStructure();

            await _service.CreateCategoryAsync(dto);
            response.data = new { Message = "New Category Added Successfully." };
            response.success = true;

            return StatusCode(201, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Category).Name, eUserPermission.HasCreateOrUpdatePermission);

            var response = new ResponseStructure();

            await _service.UpdateCategoryAsync(dto);
            response.data = new { Message = "Category Modified Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpPut("AddSubCategory/{id}")]
        public async Task<IActionResult> AddSubCategory(Guid id, [FromBody] CategoryCreateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Category).Name, eUserPermission.HasCreateOrUpdatePermission);

            var response = new ResponseStructure();

            await _service.AddSubCategoryAsync(id, dto);
            response.data = new { Message = "SubCategory Added Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpPut("RemoveSubCategory/{id}")]
        public async Task<IActionResult> RemoveSubCategory(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Category).Name, eUserPermission.HasCreateOrUpdatePermission);

            var response = new ResponseStructure();

            await _service.RemoveSubCategoryAsync(id);
            response.data = new { Message = "SubCategory Removed Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Category).Name, eUserPermission.HasDeletePermission);

            var response = new ResponseStructure();

            await _service.DeleteCategoryAsync(id);
            response.data = new { Message = $"Category with Id = {id} is Deleted Successfully." };
            response.success = true;

            return StatusCode(200, response);
        }
    }
}