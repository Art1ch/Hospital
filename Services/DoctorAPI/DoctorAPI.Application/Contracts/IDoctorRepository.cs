using DoctorAPI.Core.Entities;
using DoctorAPI.Core.Enums;

namespace DoctorAPI.Application.Contracts;

public interface IDoctorRepository
{
    Task<List<DoctorEntity>> GetAll(int page, int pageSize, CancellationToken ct);
    Task<DoctorEntity> GetById<T>(T id, CancellationToken ct);
    Task<DoctorEntity> GetBySpecialization<T>(T specializationId, CancellationToken ct);
    Task<List<DoctorEntity>> GetByStatus(StatusEnum status, CancellationToken ct);
    Task Create(DoctorEntity doctor, CancellationToken ct);
    Task<T> Update<T>(DoctorEntity doctor, CancellationToken ct);
    Task Delete<T>(T id, CancellationToken ct);
}
