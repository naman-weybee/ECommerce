using ECommerce.Application.Interfaces;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.ServiceHelper
{
    public class ServiceHelper<TEntity> : IServiceHelper<TEntity>
        where TEntity : class
    {
        private readonly IRepository<TEntity> _repository;

        public ServiceHelper(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<List<TEntity>> GetAllAsync(RequestParams? requestParams = null, IQueryable<TEntity>? query = null)
        {
            return (requestParams, query) switch
            {
                (not null, not null) =>
                    (await _repository.GetAllAsync(requestParams!, query))?.ToList()!,

                (not null, null) =>
                    (await _repository.GetAllAsync(requestParams!))?.ToList()!,

                (null, not null) =>
                    await _repository.GetAllAsync(query),

                _ =>
                    await _repository.GetAllAsync(),
            };
        }

        public async Task<TEntity> GetByIdAsync(Guid id, IQueryable<TEntity>? query = null)
        {
            return query switch
            {
                (not null) =>
                    await _repository.GetByIdAsync(id, query),

                _ =>
                    await _repository.GetByIdAsync(id),
            };
        }

        public async Task<TEntity> GetByQueryAsync(IQueryable<TEntity> query)
        {
            return await _repository.GetByPropertyAsync(query);
        }
    }
}