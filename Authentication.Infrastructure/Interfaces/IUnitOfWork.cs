namespace Authentication.Infrastructure.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
    void BeginTransaction();
    Task CommitAsync();
    Task RollbackAsync();
}
