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
            var data = await _service.GetAllCategoriesAsync(requestParams);
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
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var data = await _service.GetCategoryByIdAsync(id);
            if (data != null)
            {
                _response.data = data;
                _response.success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Category).Name, eUserPermission.HasCreateOrUpdatePermission);

            await _service.CreateCategoryAsync(dto);
            _response.data = new { Message = "New Category Added Successfully." };
            _response.success = true;

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Category).Name, eUserPermission.HasCreateOrUpdatePermission);

            await _service.UpdateCategoryAsync(dto);
            _response.data = new { Message = "Category Modified Successfully." };
            _response.success = true;

            return StatusCode(200, _response);
        }

        [HttpPut("AddSubCategory/{id}")]
        public async Task<IActionResult> AddSubCategory(Guid id, [FromBody] CategoryCreateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Category).Name, eUserPermission.HasCreateOrUpdatePermission);

            await _service.AddSubCategoryAsync(id, dto);
            _response.data = new { Message = "SubCategory Added Successfully." };
            _response.success = true;

            return StatusCode(200, _response);
        }

        [HttpPut("RemoveSubCategory/{id}")]
        public async Task<IActionResult> RemoveSubCategory(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Category).Name, eUserPermission.HasCreateOrUpdatePermission);

            await _service.RemoveSubCategoryAsync(id);
            _response.data = new { Message = "SubCategory Removed Successfully." };
            _response.success = true;

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(typeof(Category).Name, eUserPermission.HasDeletePermission);

            await _service.DeleteCategoryAsync(id);
            _response.data = new { Message = $"Category with Id = {id} is Deleted Successfully." };
            _response.success = true;

            return StatusCode(200, _response);
        }
    }
}