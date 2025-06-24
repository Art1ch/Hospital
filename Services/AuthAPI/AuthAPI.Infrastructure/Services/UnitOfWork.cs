using AuthAPI.Application.Contracts.UnitOfWork;
using AuthAPI.Infrastructure.Context;

namespace AuthAPI.Infrastructure.Services;

internal class UnitOfWork : IUnitOfWork
{
    private readonly AuthDbContext _dbContext;

    public UnitOfWork(AuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_dbContext.Database.CurrentTransaction == null)
        {
            await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.Database.CommitTransactionAsync(cancellationToken);
    }


    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.Database.RollbackTransactionAsync(cancellationToken);
    }
}
