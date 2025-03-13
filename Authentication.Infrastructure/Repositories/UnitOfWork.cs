using Authentication.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Authentication.Infrastructure.Repositories;
public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext _context = context;
    private IDbContextTransaction _transaction;

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void BeginTransaction()
    {
        _transaction = _context.Database.BeginTransaction();
    }

    public async Task CommitAsync()
    {
        await _transaction.CommitAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task RollbackAsync()
    {
        await _transaction.RollbackAsync();
    }
}
