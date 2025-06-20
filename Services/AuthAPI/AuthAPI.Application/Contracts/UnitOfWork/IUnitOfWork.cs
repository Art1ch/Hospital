using AuthAPI.Application.Contracts.Repository;
using AuthAPI.Application.Contracts.Repository.Token;

namespace AuthAPI.Application.Contracts.UnitOfWork;

public interface IUnitOfWork
{
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    TRepositoryInterface GetRepository<TRepositoryInterface>();
}
