using ECommerce.Shared.RequestModel;
using X.PagedList;

namespace ECommerce.Shared.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();

        Task<List<TEntity>> GetAllAsync(IQueryable<TEntity> query);

        Task<IPagedList<TEntity>> GetAllAsync(RequestParams requestParams);

        Task<IPagedList<TEntity>> GetAllAsync(RequestParams requestParams, IQueryable<TEntity> query);

        Task<TEntity> GetByIdAsync(Guid id);

        Task<TEntity> GetByIdAsync(Guid id, IQueryable<TEntity> query);

        Task<TEntity> GetByPropertyAsync(IQueryable<TEntity> query);

        Task InsertAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync();

        IQueryable<TEntity> GetQuery();
    }
}