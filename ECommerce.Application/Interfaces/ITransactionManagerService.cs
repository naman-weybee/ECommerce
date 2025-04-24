namespace ECommerce.Application.Interfaces
{
    public interface ITransactionManagerService
    {
        Task BeginTransactionAsync();

        Task CommitTransactionAsync();

        Task RollbackTransactionAsync();

        void Dispose();
    }
}