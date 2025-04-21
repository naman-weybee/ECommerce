using ECommerce.Shared.Interfaces;
using ECommerce.Shared.RequestModel;
using X.PagedList;
using X.PagedList.Extensions;

namespace ECommerce.Application.Services
{
    public class PaginationService : IPaginationService
    {
        public IPagedList<T> SortResult<T>(IQueryable<T> source, RequestParams requestParams)
        {
            if (!string.IsNullOrEmpty(requestParams.SortBy))
            {
                var property = typeof(T).GetProperty(requestParams.SortBy);
                if (property != null)
                {
                    source = string.Equals(requestParams.OrderBy?.Trim(), "DESC", StringComparison.OrdinalIgnoreCase)
                        ? source.AsEnumerable().OrderByDescending(e => property.GetValue(e)).AsQueryable()
                        : source.AsEnumerable().OrderBy(e => property.GetValue(e)).AsQueryable();
                }
            }
            else
            {
                var nameProperty = typeof(T).GetProperty("CreatedDate");
                if (nameProperty != null)
                    source = source.AsEnumerable().OrderByDescending(e => nameProperty.GetValue(e)).AsQueryable();
            }

            return source.ToPagedList(requestParams.PageNumber, requestParams.PageSize);
        }
    }
}