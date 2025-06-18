using AuthAPI.Application.Contracts.Repository.Account;

namespace AuthAPI.Application.Contracts.UnitOfWork;

public interface IUnitOfWork
{
    public IAccountRepository AccountRepository { get; }
    public IRefreshTokenRepository RefreshTokenRepository { get; }
    public IReferenceTokenRepository ReferenceTokenRepository { get; }
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
}
