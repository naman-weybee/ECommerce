using ECommerce.Application.Interfaces;
using ECommerce.Infrastructure.Data;

namespace ECommerce.Infrastructure.ExternalServices
{
    public class DBService : IDBService
    {
        private readonly ApplicationDbContext _context;

        public DBService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IAsyncDisposable Begin()
        {
            return new EFSaveChanges(_context);
        }
    }
}