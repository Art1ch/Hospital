using AuthAPI.Application.Contracts.UnitOfWork;
using AuthAPI.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;

namespace AuthAPI.Infrastructure.Services;

internal class UnitOfWork : IUnitOfWork
{
    private readonly AuthDbContext _dbContext;
    private readonly IServiceProvider _serviceProvider;

    public UnitOfWork(
        AuthDbContext dbContext,
        IServiceProvider provider)
    {
        _dbContext = dbContext;
        _serviceProvider = provider;
    }
    
    public TRepositoryInterface GetRepository<TRepositoryInterface>()
    {
        var repository = _serviceProvider.GetService<TRepositoryInterface>();
        return repository!;
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
