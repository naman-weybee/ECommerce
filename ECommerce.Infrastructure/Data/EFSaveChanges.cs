namespace ECommerce.Infrastructure.Data
{
    public class EFSaveChanges : IAsyncDisposable
    {
        private readonly ApplicationDbContext _context;

        public EFSaveChanges(ApplicationDbContext context)
        {
            _context = context;
        }

        public async ValueTask DisposeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}