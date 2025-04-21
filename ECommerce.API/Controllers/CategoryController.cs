using ECommerce.API.Helper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
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
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var data = await _service.GetCategoryByIdAsync(id);
            if (data != null)
            {
                _response.Data = data;
                _response.Success = true;
            }

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Category, eUserPermission.HasCreateOrUpdatePermission);

            await _service.CreateCategoryAsync(dto);
            _response.Data = new { Message = "New Category Added Successfully." };
            _response.Success = true;

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Category, eUserPermission.HasCreateOrUpdatePermission);

            await _service.UpdateCategoryAsync(dto);
            _response.Data = new { Message = "Category Modified Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }

        [HttpPut("AddSubCategory/{id}")]
        public async Task<IActionResult> AddSubCategory(Guid id, [FromBody] CategoryCreateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Category, eUserPermission.HasCreateOrUpdatePermission);

            await _service.AddSubCategoryAsync(id, dto);
            _response.Data = new { Message = "SubCategory Added Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }

        [HttpPut("RemoveSubCategory/{id}")]
        public async Task<IActionResult> RemoveSubCategory(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Category, eUserPermission.HasCreateOrUpdatePermission);

            await _service.RemoveSubCategoryAsync(id);
            _response.Data = new { Message = "SubCategory Removed Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Category, eUserPermission.HasDeletePermission);

            await _service.DeleteCategoryAsync(id);
            _response.Data = new { Message = $"Category with Id = {id} is Deleted Successfully." };
            _response.Success = true;

            return StatusCode(200, _response);
        }
    }
}