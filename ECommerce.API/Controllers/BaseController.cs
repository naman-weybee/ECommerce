using ECommerce.API.Helper;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class BaseController : Controller
    {
        internal readonly IHTTPHelper _httpHelper;

        internal readonly Guid _userId;

        public BaseController(IHTTPHelper httpHelper)
        {
            _httpHelper = httpHelper;
            _userId = _httpHelper.GetUserId();
        }
    }
}