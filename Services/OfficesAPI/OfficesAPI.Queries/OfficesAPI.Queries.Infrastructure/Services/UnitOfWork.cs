using MongoDB.Driver;
using OfficesAPI.Queries.Application.Contracts.UnitOfWork;
using OfficesAPI.Queries.Infrastructure.Context;

namespace OfficesAPI.Infrastructure.Services;

internal class UnitOfWork : IUnitOfWork
{
    private readonly OfficeDbContext _context;
    private IClientSessionHandle? _session;

    public UnitOfWork(OfficeDbContext context)
    {
        _context = context;
    }

    public async Task BeginTransactionAsync(CancellationToken ct = default)
    {
        _session = await _context.Client.StartSessionAsync(cancellationToken: ct);
        _session.StartTransaction();
    }

    public async Task CommitTransactionAsync(CancellationToken ct = default)
    {
        await _session!.CommitTransactionAsync(ct);
        DisposeSession();
    }

    public async Task RollbackTransactionAsync(CancellationToken ct = default)
    {
        await _session!.AbortTransactionAsync(ct);
        DisposeSession();
    }

    private void DisposeSession()
    {
        _session?.Dispose();
        _session = null;
    }
}
