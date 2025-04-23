using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IServiceHelper<TEntity>
        where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync(RequestParams? requestParams = null, IQueryable<TEntity>? query = null);

        Task<TEntity> GetByIdAsync(Guid id, IQueryable<TEntity>? query = null);

        Task<TEntity> GetByQueryAsync(IQueryable<TEntity> query);
    }
}