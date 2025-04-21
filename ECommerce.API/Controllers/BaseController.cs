using ECommerce.API.Helper.Interfaces;
using ECommerce.Shared.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        internal readonly IHTTPHelper _httpHelper;

        internal readonly Guid _userId;

        internal ResponseStructure _response = new();

        public BaseController(IHTTPHelper httpHelper)
        {
            _httpHelper = httpHelper;
            _userId = _httpHelper.GetUserId();
        }
    }
}