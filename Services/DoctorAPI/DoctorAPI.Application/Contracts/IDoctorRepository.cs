using DoctorAPI.Core.Entities;
using DoctorAPI.Core.Enums;

namespace DoctorAPI.Application.Contracts;

public interface IDoctorRepository<TId1, TId2>
{
    Task<List<DoctorEntity<TId1, TId2>>> GetAll(int page, int pageSize, CancellationToken ct);
    Task<DoctorEntity<TId1, TId2>> GetById(TId1 id, CancellationToken ct);
    Task<DoctorEntity<TId1, TId2>> GetBySpecialization(TId2 specializationId, CancellationToken ct);
    Task<List<DoctorEntity<TId1, TId2>>> GetByStatus(StatusEnum status, CancellationToken ct);
    Task<TId1> Create(DoctorEntity<TId1, TId2> doctor, CancellationToken ct);
    Task<TId1> Update(DoctorEntity<TId1, TId2> doctor, CancellationToken ct);
    Task Delete(TId1 id, CancellationToken ct);
}

