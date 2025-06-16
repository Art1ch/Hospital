using DoctorAPI.Application.RepositoryResults.Doctor.GetAll;
using DoctorAPI.Application.RepositoryResults.Doctor.GetById;
using DoctorAPI.Application.RepositoryResults.Doctor.GetBySpecialization;
using DoctorAPI.Application.RepositoryResults.Doctor.GetByStatus;
using DoctorAPI.Application.Entities;
using DoctorAPI.Application.Enums;

namespace DoctorAPI.Application.Contracts.Repository.Doctor;

public interface IDoctorRepository : IRepository<DoctorEntity, Guid>
{
    Task<List<GetAllDoctorsResult>> GetAllDoctorsAsync(int page, int pageSize, CancellationToken ct);
    Task<GetDoctorInfoByIdResult> GetDoctorInfoById(Guid id, CancellationToken ct);
    Task<GetDoctorBySpecializationResult> GetDoctorBySpecializationAsync(int specializationId, CancellationToken ct);
    Task<List<GetDoctorsByStatusResult>> GetDoctorByStatusAsync(DoctorStatus status, CancellationToken ct);
}
