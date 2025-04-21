using ECommerce.Domain.Aggregates;
using ECommerce.Infrastructure.Data;
using ECommerce.Shared.Interfaces;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace ECommerce.Infrastructure.Repositories
{
    public class Repository<TAggregate, TEntity> : IRepository<TAggregate, TEntity>
        where TAggregate : AggregateRoot<TEntity>
        where TEntity : class
    {
        internal ApplicationDbContext _context;
        internal DbSet<TEntity> DbSet;
        private readonly IPaginationService _pagination;

        public Repository(ApplicationDbContext context, IPaginationService pagination)
        {
            _context = context;
            DbSet = context.Set<TEntity>();
            _pagination = pagination;
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id, IQueryable<TEntity>? query = null)
        {
            query ??= _context.Set<TEntity>().AsQueryable();

            var entity = await query.SingleOrDefaultAsync(e => EF.Property<Guid>(e, "Id").Equals(id));

            if (entity != null)
                return entity;

            throw new InvalidOperationException($"Data for Id = {id} is not available.");
        }

        public async Task<IPagedList<TEntity>> GetAllAsync(RequestParams requestParams, IQueryable<TEntity>? query = null)
        {
            query ??= _context.Set<TEntity>().AsQueryable();

            if (!string.IsNullOrEmpty(requestParams.search))
            {
                var nameProperty = typeof(TEntity).GetProperty("Name");
                if (nameProperty != null)
                {
                    var searchTerm = $"%{requestParams.search}%";
                    query = query.Where(e => EF.Functions.Like(EF.Property<string>(e, "Name"), searchTerm));
                }
            }

            requestParams.recordCount = await query.CountAsync();

            return _pagination.SortResult(query, requestParams);
        }

        public virtual async Task InsertAsync(TAggregate aggregate)
        {
            var entity = aggregate.Entity;

            DbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TAggregate aggregate)
        {
            var entity = aggregate.Entity;

            DbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            if (entity != null)
            {
                DbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Data is not Available...!");
            }
        }

        public virtual async Task SaveAsync(TEntity entity)
        {
            DbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual IQueryable<TEntity> GetDbSet()
        {
            return DbSet = _context.Set<TEntity>();
        }
    }
}