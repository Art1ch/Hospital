using DoctorAPI.Application.Contracts.Repository.Doctor;
using DoctorAPI.Application.RepositoryResults.Doctor.GetAll;
using DoctorAPI.Application.RepositoryResults.Doctor.GetById;
using DoctorAPI.Application.RepositoryResults.Doctor.GetBySpecialization;
using DoctorAPI.Application.RepositoryResults.Doctor.GetByStatus;
using DoctorAPI.Core.Entities;
using DoctorAPI.Core.Enums;
using DoctorAPI.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DoctorAPI.Infrastructure.Repositories;

internal class DoctorRepository : IDoctorRepository
{
    private readonly DoctorDbContext _dbContext;

    public DoctorRepository(DoctorDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetAllDoctorsResult>> GetAllAsync(int page, int pageSize, CancellationToken ct)
    {
        var a = _dbContext.Doctors.Count();
        var doctorInfos = await _dbContext.Doctors
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(d => new GetAllDoctorsResult(
                d.Id,
                d.FirstName,
                d.LastName,
                d.MiddleName))
            .ToListAsync(ct);

        return doctorInfos;
    }

    public async Task<GetByIdDoctorResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var doctor = await _dbContext.Doctors
            .AsNoTracking()
            .Include(d => d.Specialization)
            .Where(d => d.Id == id)
            .Select(d => new GetByIdDoctorResult(
                d.Id,
                d.FirstName,
                d.LastName,
                d.MiddleName,
                d.Status,
                d.BirthDate,
                d.CareerStartDay,
                d.Specialization))
            .FirstOrDefaultAsync(cancellationToken);

        return doctor ?? throw new KeyNotFoundException();
    }

    public async Task<GetBySpecializationResult> GetBySpecializationAsync(int specializationId, CancellationToken cancellationToken)
    {
        var doctorInfo = await _dbContext.Doctors
            .AsNoTracking()
            .Where(d => d.SpecializationId == specializationId)
            .Select(d => new GetBySpecializationResult(
                d.Id,
                d.FirstName,
                d.LastName,
                d.MiddleName))
            .FirstOrDefaultAsync(cancellationToken);
        return doctorInfo ?? throw new KeyNotFoundException();
    }

    public async Task<List<GetByStatusResult>> GetByStatusAsync(DoctorStatus status, CancellationToken cancellationToken)
    {
        var doctorInfos = await _dbContext.Doctors
            .Where(d => d.Status == status)
            .Select(d => new GetByStatusResult(
                d.Id,
                d.FirstName,
                d.LastName,
                d.MiddleName))
            .ToListAsync(cancellationToken);
        return doctorInfos;
    }

    public async Task CreateAsync(DoctorEntity doctor, CancellationToken cancellationToken)
    {
        await _dbContext.Doctors.AddAsync(doctor, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

    }

    public async Task UpdateAsync(DoctorEntity doctor, CancellationToken cancellationToken)
    {
        await _dbContext.Doctors
            .Where(d => d.Id!.Equals(doctor.Id))
            .ExecuteUpdateAsync(s => s
                .SetProperty(d => d.FirstName, doctor.FirstName)
                .SetProperty(d => d.LastName, doctor.LastName)
                .SetProperty(d => d.MiddleName, doctor.MiddleName)
                .SetProperty(d => d.Status, doctor.Status)
                .SetProperty(d => d.BirthDate, doctor.BirthDate)
                .SetProperty(d => d.CareerStartDay, doctor.CareerStartDay),
                cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        await _dbContext.Specializations
            .Where(s => s.Id == doctor.SpecializationId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(s => s.Name, doctor.Specialization.Name),
                cancellationToken);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        
        await _dbContext.Doctors
            .Where(d => d.Id!.Equals(id))
            .ExecuteDeleteAsync(cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
        
    }
}