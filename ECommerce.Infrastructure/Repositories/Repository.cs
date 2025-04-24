using ECommerce.Infrastructure.Data;
using ECommerce.Shared.Interfaces;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace ECommerce.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<TEntity> DbSet;
        private readonly IPaginationService _pagination;

        public Repository(ApplicationDbContext context, IPaginationService pagination)
        {
            _context = context;
            DbSet = context.Set<TEntity>();
            _pagination = pagination;
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return (await _context.Set<TEntity>().FindAsync(id))!;
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id, IQueryable<TEntity> query)
        {
            return (await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id").Equals(id)))!;
        }

        public virtual async Task<TEntity> GetByPropertyAsync(IQueryable<TEntity> query)
        {
            return (await query.FirstOrDefaultAsync())!;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>()?.ToListAsync()!;
        }

        public async Task<List<TEntity>> GetAllAsync(IQueryable<TEntity> query)
        {
            return await query?.ToListAsync()!;
        }

        public async Task<IPagedList<TEntity>> GetAllAsync(RequestParams requestParams)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (!string.IsNullOrEmpty(requestParams.Search))
            {
                var nameProperty = typeof(TEntity).GetProperty("Name");
                if (nameProperty != null)
                {
                    var searchTerm = $"%{requestParams.Search}%";
                    query = query.Where(e => EF.Functions.Like(EF.Property<string>(e, "Name"), searchTerm));
                }
            }

            requestParams.RecordCount = await query.CountAsync();

            return _pagination.SortResult(query, requestParams);
        }

        public async Task<IPagedList<TEntity>> GetAllAsync(RequestParams requestParams, IQueryable<TEntity> query)
        {
            if (!string.IsNullOrEmpty(requestParams.Search))
            {
                var nameProperty = typeof(TEntity).GetProperty("Name");
                if (nameProperty != null)
                {
                    var searchTerm = $"%{requestParams.Search}%";
                    query = query.Where(e => EF.Functions.Like(EF.Property<string>(e, "Name"), searchTerm));
                }
            }

            requestParams.RecordCount = await query.CountAsync();

            return _pagination.SortResult(query, requestParams);
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public virtual void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity != null)
                DbSet.Remove(entity);
            else
                throw new InvalidOperationException("Data is not Available...!");
        }

        public virtual Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public virtual IQueryable<TEntity> GetQuery()
        {
            return DbSet = _context.Set<TEntity>();
        }
    }
}