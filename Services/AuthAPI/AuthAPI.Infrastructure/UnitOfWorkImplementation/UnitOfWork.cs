using AuthAPI.Application.Contracts.Repository.Account;
using AuthAPI.Application.Contracts.UnitOfWork;
using AuthAPI.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Infrastructure.UnitOfWorkImplementation;

internal class UnitOfWork : IUnitOfWork
{
    private readonly AuthDbContext _dbContext;
    public IAccountRepository AccountRepository { get; set; }

    public IRefreshTokenRepository RefreshTokenRepository { get; set; }

    public IReferenceTokenRepository ReferenceTokenRepository { get; set; }

    public UnitOfWork(
        AuthDbContext dbContext,
        IAccountRepository accountRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IReferenceTokenRepository referenceTokenRepository)
    {
        _dbContext = dbContext;
        AccountRepository = accountRepository;
        RefreshTokenRepository = refreshTokenRepository;
        ReferenceTokenRepository = referenceTokenRepository;
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_dbContext.Database.CurrentTransaction != null)
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
