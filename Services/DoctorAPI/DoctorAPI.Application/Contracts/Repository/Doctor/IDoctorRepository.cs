using DoctorAPI.Application.RepositoryResults.Doctor.GetAll;
using DoctorAPI.Application.RepositoryResults.Doctor.GetById;
using DoctorAPI.Application.RepositoryResults.Doctor.GetBySpecialization;
using DoctorAPI.Application.RepositoryResults.Doctor.GetByStatus;
using DoctorAPI.Core.Entities;
using DoctorAPI.Core.Enums;

namespace DoctorAPI.Application.Contracts.Repository.Doctor;

public interface IDoctorRepository : IRepository<DoctorEntity, Guid>
{
    Task<List<GetAllDoctorsResult>> GetAllAsync(int page, int pageSize, CancellationToken ct);
    Task<GetByIdDoctorResult> GetByIdAsync(Guid id, CancellationToken ct);
    Task<GetBySpecializationResult> GetBySpecializationAsync(int specializationId, CancellationToken ct);
    Task<List<GetByStatusResult>> GetByStatusAsync(DoctorStatus status, CancellationToken ct);
}
