using DoctorAPI.Application.Contracts.Repository.Doctor;
using DoctorAPI.Application.RepositoryResults.Doctor.GetAll;
using DoctorAPI.Application.RepositoryResults.Doctor.GetById;
using DoctorAPI.Application.RepositoryResults.Doctor.GetBySpecialization;
using DoctorAPI.Application.RepositoryResults.Doctor.GetByStatus;
using DoctorAPI.Application.Entities;
using DoctorAPI.Application.Enums;
using DoctorAPI.Infrastructure.Context;
using DoctorAPI.Infrastructure.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DoctorAPI.Infrastructure.Repositories.Implementations;

internal class DoctorRepository : Repository<DoctorEntity, Guid>, IDoctorRepository 
{
    private readonly DoctorDbContext _dbContext;

    public DoctorRepository(DoctorDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetAllDoctorsResult>> GetAllDoctorsAsync(int page, int pageSize, CancellationToken ct)
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

    public async Task<GetDoctorInfoByIdResult> GetDoctorInfoById(Guid id, CancellationToken cancellationToken)
    {
        var doctor = await _dbContext.Doctors
            .AsNoTracking()
            .Include(d => d.Specialization)
            .Where(d => d.Id == id)
            .Select(d => new GetDoctorInfoByIdResult(
                d.Id,
                d.AccountId,
                d.FirstName,
                d.LastName,
                d.MiddleName,
                d.Status,
                d.BirthDate,
                d.CareerStartDay,
                d.Specialization))
            .FirstAsync(cancellationToken);

        return doctor;
    }

    public async Task<GetDoctorBySpecializationResult> GetDoctorBySpecializationAsync(int specializationId, CancellationToken cancellationToken)
    {
        var doctorInfo = await _dbContext.Doctors
            .AsNoTracking()
            .Where(d => d.SpecializationId == specializationId)
            .Select(d => new GetDoctorBySpecializationResult(
                d.Id,
                d.FirstName,
                d.LastName,
                d.MiddleName))
            .FirstAsync(cancellationToken);
        return doctorInfo;
    }

    public async Task<List<GetDoctorsByStatusResult>> GetDoctorByStatusAsync(DoctorStatus status, CancellationToken cancellationToken)
    {
        var doctorInfos = await _dbContext.Doctors
            .Where(d => d.Status == status)
            .Select(d => new GetDoctorsByStatusResult(
                d.Id,
                d.FirstName,
                d.LastName,
                d.MiddleName))
            .ToListAsync(cancellationToken);
        return doctorInfos;
    }
}