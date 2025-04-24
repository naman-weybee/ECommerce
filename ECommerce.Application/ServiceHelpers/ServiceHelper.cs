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
            switch ((requestParams, query))
            {
                case (not null, not null):
                    return (await _repository.GetAllAsync(requestParams!, query))?.ToList()!;

                case (not null, null):
                    return (await _repository.GetAllAsync(requestParams!))?.ToList()!;

                case (null, not null):
                    return await _repository.GetAllAsync(query);

                default:
                    return await _repository.GetAllAsync();
            }
        }

        public async Task<TEntity> GetByIdAsync(Guid id, IQueryable<TEntity>? query = null)
        {
            switch (query)
            {
                case (not null):
                    return await _repository.GetByIdAsync(id, query);

                default:
                    return await _repository.GetByIdAsync(id);
            }
        }

        public async Task<TEntity> GetByQueryAsync(IQueryable<TEntity> query)
        {
            return await _repository.GetByPropertyAsync(query);
        }

        public async Task<TEntity> DeleteAsync(IQueryable<TEntity> query)
        {
            return await _repository.GetByPropertyAsync(query);
        }
    }
}