using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace ECommerce.API.Conventions
{
    public class RoutePrefixConvention : IControllerModelConvention
    {
        private readonly string _prefix;

        public RoutePrefixConvention(string prefix)
        {
            _prefix = prefix;
        }

        public void Apply(ControllerModel controller)
        {
            var selector = controller.Selectors.FirstOrDefault();
            if (selector == null)
                return;

            var routeTemplate = $"{_prefix}/{controller.ControllerName}";
            selector.AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(routeTemplate));
        }
    }
}