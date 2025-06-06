using DoctorAPI.Core.Entities;
using DoctorAPI.Core.Enums;

namespace DoctorAPI.Application.Contracts;

public interface IDoctorRepository<TDoctorId, TSpecializationId>
{
    Task<List<DoctorEntity<TDoctorId, TSpecializationId>>> GetAllAsync(
        int page, int pageSize, CancellationToken ct);
    Task<DoctorEntity<TDoctorId, TSpecializationId>> GetByIdAsync(
        TDoctorId id, CancellationToken ct);
    Task<DoctorEntity<TDoctorId, TSpecializationId>> GetBySpecializationAsync(
        TSpecializationId specializationId, CancellationToken ct);
    Task<List<DoctorEntity<TDoctorId, TSpecializationId>>> GetByStatusAsync(
        StatusEnum status, CancellationToken ct);
    Task<TDoctorId> CreateAsync(
        DoctorEntity<TDoctorId, TSpecializationId> doctor, CancellationToken ct);
    Task<TDoctorId> UpdateAsync(
        DoctorEntity<TDoctorId, TSpecializationId> doctor, CancellationToken ct);
    Task DeleteAsync(TDoctorId id, CancellationToken ct);
}