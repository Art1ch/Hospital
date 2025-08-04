using AppointmentAPI.Application.Contracts.UnitOfWork;
using System.Data;

namespace AppointmentAPI.Infrastructure.Services.UnitOfWork;

internal sealed class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly IDbConnection _connection;
    private IDbTransaction _transaction;

    public UnitOfWork(IDbConnection connection)
    {
        _connection = connection;
    }

    public Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }
        _transaction = _connection.BeginTransaction();
        return Task.CompletedTask;
    }

    public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction.Commit();
        return Task.CompletedTask;
    }

    public Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction?.Rollback();
        return Task.CompletedTask;
    }
    public void Dispose()
    {
        _transaction?.Dispose();
        _connection?.Dispose();
    }
}
