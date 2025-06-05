using DoctorAPI.Application.Contracts;
using DoctorAPI.Core.Entities;
using DoctorAPI.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace DoctorAPI.Infrastructure.Repositories;

public class SpecializationRepository : ISpecializationRepository
{
    private readonly DoctorDbContext _dbContext;

    public SpecializationRepository(DoctorDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T> Create<T>(SpecializationEntity specialization, CancellationToken ct)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);
        try
        {
            await _dbContext.Specializations.AddAsync(specialization, ct);
            await _dbContext.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);
            return (T)(object)specialization.Id;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
    }

    public async Task Delete<T>(T id, CancellationToken ct)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);
        try
        {
            await _dbContext.Specializations
                .Where(s => s.Id.Equals(id))
                .ExecuteDeleteAsync(ct);
            await _dbContext.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
    }

    public async Task<SpecializationEntity> Get<T>(T id, CancellationToken ct)
    {
        var specialization = await _dbContext.Specializations
           .AsNoTracking()
           .FirstOrDefaultAsync(s => s.Id.Equals(id), ct);
        return specialization ?? throw new NullReferenceException();
    }

    public async Task<List<SpecializationEntity>> GetAll(int page, int pageSize, CancellationToken ct)
    {
        var specializations = await _dbContext.Specializations
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync(ct);
        return specializations;
    }

    public async Task<T> Update<T>(SpecializationEntity specialization, CancellationToken ct)
    {
        if (specialization == null)
            throw new ArgumentNullException();

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);
        try
        {
            await _dbContext.Specializations
                .Where(d => d.Id.Equals(specialization.Id))
                .ExecuteUpdateAsync(s => s
                .SetProperty(sp => sp.Name, specialization.Name)
                .SetProperty(sp => sp.Doctor, specialization.Doctor),
                ct);
            await _dbContext.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);
            return (T)(object)specialization.Id;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
    }
}
