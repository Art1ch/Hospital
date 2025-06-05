using DoctorAPI.Application.Contracts;
using DoctorAPI.Core.Entities;
using DoctorAPI.Core.Enums;
using DoctorAPI.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace DoctorAPI.Infrastructure.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly DoctorDbContext _dbContext;

    public DoctorRepository(DoctorDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(DoctorEntity doctor, CancellationToken ct)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);
        try
        {
            await _dbContext.Doctors.AddAsync(doctor, ct);
            await _dbContext.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
    }

    public async Task Delete<T>(T id, CancellationToken ct)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);
        try
        {
            await _dbContext.Doctors.Where(d => d.Id.Equals(id)).ExecuteDeleteAsync(ct);
            await _dbContext.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
    }

    public async Task<List<DoctorEntity>> GetAll(int page, int pageSize, CancellationToken ct)
    {
        var doctors = await _dbContext.Doctors
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();
        return doctors;
    }

    public async Task<DoctorEntity> GetById<T>(T id, CancellationToken ct)
    {
        var doctor = await _dbContext.Doctors
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id.Equals(id));
        return doctor ?? throw new NullReferenceException();
    }

    public async Task<DoctorEntity> GetBySpecialization(SpecializationEntity specialization, CancellationToken ct)
    {
        var doctor = await _dbContext.Doctors
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Specialization.Equals(specialization));
        return doctor ?? throw new NullReferenceException();
    }

    public async Task<List<DoctorEntity>> GetByStatus(StatusEnum status, CancellationToken ct)
    {
        var doctors = await _dbContext.Doctors
            .AsNoTracking()
            .Where(d => d.Status.Equals(status))
            .ToListAsync();
        return doctors;
    }

    public async Task<T> Update<T>(DoctorEntity doctor, CancellationToken ct)
    {
        if (doctor == null)
            throw new ArgumentNullException();

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);
        try
        {
            await _dbContext.Doctors
                .Where(d => d.Id.Equals(doctor.Id))
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
            return (T)(object)doctor.Id;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
    }
}
