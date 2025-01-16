using ECommerce.Domain.Aggregates;
using ECommerce.Shared.RequestModel;
using X.PagedList;

namespace ECommerce.Shared.Repositories
{
    public interface IRepository<TAggregate, TEntity>
        where TAggregate : AggregateRoot<TEntity>
        where TEntity : class
    {
        Task<TEntity> GetByIdAsync(Guid id, IQueryable<TEntity>? query = null);

        Task<IPagedList<TEntity>> GetAllAsync(RequestParams requestParams, IQueryable<TEntity>? query = null);

        Task InsertAsync(TAggregate aggregate);

        Task UpdateAsync(TAggregate aggregate);

        Task DeleteAsync(TEntity entity);

        Task SaveAsync(TEntity entity);

        IQueryable<TEntity> GetDbSet();
    }
}