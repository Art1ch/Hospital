using DoctorAPI.Application.Contracts.UnitOfWork;
using DoctorAPI.Infrastructure.Context;

namespace DoctorAPI.Infrastructure.UnitOfWorkImplementation;

internal class UnitOfWork : IUnitOfWork
{
    private readonly DoctorDbContext _dbContext;

    public UnitOfWork(DoctorDbContext dbContext)
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

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.Database.CommitTransactionAsync(cancellationToken);
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.Database.RollbackTransactionAsync(cancellationToken);
    }
}
