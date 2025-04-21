using ECommerce.API.Helper.Interfaces;
using ECommerce.Shared.RequestModel;
using ECommerce.Shared.ResponseModel;

namespace ECommerce.API.Helper
{
    public class ControllerHelper : IControllerHelper
    {
        public void SetResponse(ResponseStructure response, string? message)
        {
            if (string.IsNullOrEmpty(message))
                return;

            response.Data = new { Message = message };
            response.Success = true;
        }

        public void SetResponse<T>(ResponseStructure response, T? data)
        {
            if (data == null)
                return;

            response.Data = data;
            response.Success = true;
        }

        public void SetResponse<T>(ResponseStructure response, List<T>? data, RequestParams? requestParams)
        {
            if (data == null)
                return;

            response.Success = true;

            if (requestParams == null)
            {
                response.Data = data;
                return;
            }

            response.Data = new ResponseMetadata<object>
            {
                PageNumber = requestParams.PageNumber,
                PageSize = requestParams.PageSize,
                Records = data,
                TotalRecordsCount = requestParams.RecordCount
            };
        }
    }
}