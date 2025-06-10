using DoctorAPI.Application.Contracts;
using DoctorAPI.Application.Dto_s.RepoDto_s.Doctor.Create;
using DoctorAPI.Application.Dto_s.RepoDto_s.Doctor.Delete;
using DoctorAPI.Application.Dto_s.RepoDto_s.Doctor.Update;
using DoctorAPI.Application.WebDto_s.Doctor.GetAll;
using DoctorAPI.Application.WebDto_s.Doctor.GetById;
using DoctorAPI.Application.WebDto_s.Doctor.GetBySpecialization;
using DoctorAPI.Application.WebDto_s.Doctor.GetByStatus;
using DoctorAPI.Core.Entities;
using DoctorAPI.Core.Enums;
using DoctorAPI.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DoctorAPI.Infrastructure.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly DoctorDbContext _dbContext;

    public DoctorRepository(
        DoctorDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetAllDoctorsRepoDto>> GetAllAsync(
        int page, int pageSize, CancellationToken ct)
    {
        var a = _dbContext.Doctors.Count();
        var doctorInfos = await _dbContext.Doctors
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .Select(d => new GetAllDoctorsRepoDto(
                d.Id,
                d.FirstName,
                d.LastName,
                d.MiddleName))
            .ToListAsync(ct);

        return doctorInfos;
    }

    public async Task<GetByIdDoctorRepoDto> GetByIdAsync
        (Guid id, CancellationToken ct)
    {
        var doctor = await _dbContext.Doctors
            .Include(d => d.Specialization)
            .Where(d => d.Id == id)
            .Select(d => new GetByIdDoctorRepoDto(
               d.Id,
               d.FirstName,
               d.LastName,
               d.MiddleName,
               d.Status,
               d.BirthDate,
               d.CareerStartDay,
               d.Specialization))
            .AsNoTracking()
            .FirstOrDefaultAsync(ct);

        return doctor ?? throw new NullReferenceException();
    }

    public async Task<GetBySpecializationRepoDto> GetBySpecializationAsync(
        int specializationId, CancellationToken ct)
    {
        var doctorInfo = await _dbContext.Doctors
            .Select(d => new GetBySpecializationRepoDto(
                d.Id,
                d.FirstName,
                d.LastName,
                d.MiddleName))
            .AsNoTracking()
            .FirstOrDefaultAsync(ct);
        return doctorInfo ?? throw new NullReferenceException();
    }

    public async Task<List<GetByStatusRepoDto>> GetByStatusAsync(
        StatusEnum status, CancellationToken ct)
    {
        var doctorInfos = await _dbContext.Doctors
            .Where(d => d.Status == status)
            .Select(d => new GetByStatusRepoDto(
                d.Id,
                d.FirstName,
                d.LastName,
                d.MiddleName))
            .ToListAsync(ct);
        return doctorInfos;
    }

    public async Task<CreateDoctorRepoDto> CreateAsync(
        DoctorEntity doctor,
        CancellationToken ct)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);
        try
        {
            await _dbContext.Doctors.AddAsync(doctor, ct);
            await _dbContext.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);
            var result = new CreateDoctorRepoDto(doctor.Id);
            return result;
        }
        catch
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
    }

    public async Task<UpdateDoctorRepoDto> UpdateAsync(
        DoctorEntity doctor,
        CancellationToken ct)
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
                    .SetProperty(d => d.CareerStartDay, doctor.CareerStartDay),
                    ct);

            await _dbContext.SaveChangesAsync(ct);

            await _dbContext.Specializations
                .Where(s => s.Id == doctor.SpecializationId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(s => s.Name, doctor.Specialization.Name),
                    ct);

            await _dbContext.SaveChangesAsync();

            await transaction.CommitAsync(ct);
            var response = new UpdateDoctorRepoDto(doctor.Id);
            return response;
        }
        catch
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
    }

    public async Task<DeleteDoctorRepoDto> DeleteAsync(Guid id, CancellationToken ct)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);
        try
        {
            await _dbContext.Doctors
                .Where(d => d.Id!.Equals(id))
                .ExecuteDeleteAsync(ct);

            await _dbContext.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);
            return new DeleteDoctorRepoDto();
        }
        catch
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
    }
}