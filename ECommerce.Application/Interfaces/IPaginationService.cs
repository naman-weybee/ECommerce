﻿using ECommerce.Shared.RequestModel;
using X.PagedList;

namespace ECommerce.Shared.Interfaces
{
    public interface IPaginationService
    {
        Task<IPagedList<T>> SortResult<T>(IQueryable<T> source, RequestParams requestParams);
    }
}