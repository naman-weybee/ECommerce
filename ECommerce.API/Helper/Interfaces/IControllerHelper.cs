using ECommerce.Shared.RequestModel;
using ECommerce.Shared.ResponseModel;

namespace ECommerce.API.Helper.Interfaces
{
    public interface IControllerHelper
    {
        void SetResponse<T>(ResponseStructure response, T? data);

        void SetResponse<T>(ResponseStructure response, List<T>? data, RequestParams? requestParams);
    }
}