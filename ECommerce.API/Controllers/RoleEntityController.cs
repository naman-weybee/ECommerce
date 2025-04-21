using ECommerce.API.Helper.Interfaces;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Enums;
using ECommerce.Shared.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class RoleEntityController : BaseController
    {
        private readonly IRoleEntityService _service;
        private readonly IControllerHelper _controllerHelper;

        public RoleEntityController(IHTTPHelper httpHelper, IRoleEntityService service, IControllerHelper controllerHelper)
            : base(httpHelper)
        {
            _service = service;
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoleEntities([FromQuery] RequestParams requestParams)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.RoleEntity, eUserPermission.HasViewPermission);

            var data = await _service.GetAllRoleEntitiesAsync(requestParams);
            _controllerHelper.SetResponse(_response, data, requestParams);

            return StatusCode(200, _response);
        }
    }
}