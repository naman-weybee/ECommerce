using ECommerce.API.Helper.Interfaces;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Enums;
using ECommerce.Shared.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class OrderStatusController : BaseController
    {
        private readonly IOrderStatusService _service;
        private readonly IControllerHelper _controllerHelper;

        public OrderStatusController(IHTTPHelper httpHelper, IOrderStatusService service, IControllerHelper controllerHelper)
            : base(httpHelper)
        {
            _service = service;
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderStatus([FromQuery] RequestParams requestParams)
        {
            await _httpHelper.ValidateUserAuthorization(eRoleEntity.OrderStatus, eUserPermission.HasViewPermission);

            var data = await _service.GetAllOrderStatusAsync(requestParams);
            _controllerHelper.SetResponse(_response, data, requestParams);

            return StatusCode(200, _response);
        }
    }
}