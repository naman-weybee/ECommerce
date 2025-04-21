using ECommerce.API.Helper.Interfaces;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Enums;
using ECommerce.Shared.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _service;
        private readonly IControllerHelper _controllerHelper;

        public CategoryController(IHTTPHelper httpHelper, ICategoryService service, IControllerHelper controllerHelper)
            : base(httpHelper)
        {
            _service = service;
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] RequestParams requestParams)
        {
            var data = await _service.GetAllCategoriesAsync(requestParams);
            _controllerHelper.SetResponse(_response, data, requestParams);

            return StatusCode(200, _response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var data = await _service.GetCategoryByIdAsync(id);
            _controllerHelper.SetResponse(_response, data);

            return StatusCode(200, _response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Category, eUserPermission.HasCreateOrUpdatePermission);

            await _service.CreateCategoryAsync(dto);
            _controllerHelper.SetResponse(_response, "New Category Added Successfully.");

            return StatusCode(201, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Category, eUserPermission.HasCreateOrUpdatePermission);

            await _service.UpdateCategoryAsync(dto);
            _controllerHelper.SetResponse(_response, "Category Modified Successfully.");

            return StatusCode(200, _response);
        }

        [HttpPut("AddSubCategory/{id}")]
        public async Task<IActionResult> AddSubCategory(Guid id, [FromBody] CategoryCreateDTO dto)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Category, eUserPermission.HasCreateOrUpdatePermission);

            await _service.AddSubCategoryAsync(id, dto);
            _controllerHelper.SetResponse(_response, "SubCategory Added Successfully.");

            return StatusCode(200, _response);
        }

        [HttpPut("RemoveSubCategory/{id}")]
        public async Task<IActionResult> RemoveSubCategory(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Category, eUserPermission.HasCreateOrUpdatePermission);

            await _service.RemoveSubCategoryAsync(id);
            _controllerHelper.SetResponse(_response, "SubCategory Removed Successfully.");

            return StatusCode(200, _response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.Category, eUserPermission.HasDeletePermission);

            await _service.DeleteCategoryAsync(id);
            _controllerHelper.SetResponse(_response, $"Category with Id = {id} is Deleted Successfully.");

            return StatusCode(200, _response);
        }
    }
}