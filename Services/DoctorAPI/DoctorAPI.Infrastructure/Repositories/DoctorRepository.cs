using DoctorAPI.Application.Contracts;
using DoctorAPI.Core.Entities;
using DoctorAPI.Core.Enums;
using DoctorAPI.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DoctorAPI.Infrastructure.Repositories;

public class DoctorRepository<TDoctorId, TSpecializationId> :
    IDoctorRepository<TDoctorId, TSpecializationId>
{
    private readonly DoctorDbContext<TDoctorId, TSpecializationId> _dbContext;

    public DoctorRepository(
        DoctorDbContext<TDoctorId, TSpecializationId> dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<DoctorEntity<TDoctorId, TSpecializationId>>> GetAllAsync(
        int page, int pageSize, CancellationToken ct)
    {
        return await _dbContext.Doctors
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<DoctorEntity<TDoctorId, TSpecializationId>> GetByIdAsync(
        TDoctorId id,
        CancellationToken ct)
    {
        var doctor = await _dbContext.Doctors
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id!.Equals(id), ct);
        return doctor ?? throw new NullReferenceException();
    }

    public async Task<DoctorEntity<TDoctorId, TSpecializationId>> GetBySpecializationAsync(
        TSpecializationId specializationId, CancellationToken ct)
    {
        var doctor = await _dbContext.Doctors
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.SpecializationId!.Equals(specializationId), ct);
        return doctor ?? throw new NullReferenceException();
    }

    public async Task<List<DoctorEntity<TDoctorId, TSpecializationId>>> GetByStatusAsync(
        StatusEnum status, CancellationToken ct)
    {
        return await _dbContext.Doctors
            .Where(d => d.Status == status)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<TDoctorId> CreateAsync(
        DoctorEntity<TDoctorId, TSpecializationId> doctor, CancellationToken ct)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);
        try
        {
            await _dbContext.Doctors.AddAsync(doctor, ct);
            await _dbContext.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);
            return doctor.Id;
        }
        catch
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
    }

    public async Task<TDoctorId> UpdateAsync(
        DoctorEntity<TDoctorId, TSpecializationId> doctor, CancellationToken ct)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);
        try
        {
            await _dbContext.Doctors
                .Where(d => d.Id!.Equals(doctor.Id))
                .ExecuteUpdateAsync(s => s
                    .SetProperty(d => d.FirstName, doctor.FirstName)
                    .SetProperty(d => d.LastName, doctor.LastName)
                    .SetProperty(d => d.MiddleName, doctor.MiddleName)
                    .SetProperty(d => d.Status, doctor.Status)
                    .SetProperty(d => d.BirthDate, doctor.BirthDate)
                    .SetProperty(d => d.CareerStartDay, doctor.CareerStartDay)
                    .SetProperty(d => d.Specialization, doctor.Specialization),
                    ct);

            await _dbContext.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);
            return doctor.Id;
        }
        catch
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
    }

    public async Task DeleteAsync(
        TDoctorId id, CancellationToken ct)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);
        try
        {
            await _dbContext.Doctors
                .Where(d => d.Id!.Equals(id))
                .ExecuteDeleteAsync(ct);

            await _dbContext.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);
        }
        catch
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
    }
}

