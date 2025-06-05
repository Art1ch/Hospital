using DoctorAPI.Core.Entities;

namespace DoctorAPI.Application.Contracts;

public interface IDoctorRepository
{
    Task<List<DoctorEntity>> GetAll(int page, int pageSize, CancellationToken ct);
    Task<DoctorEntity> Get<T>(T id, CancellationToken ct);
    Task<T> Create<T>(T id, CancellationToken ct);
    Task<T> Update<T>(DoctorEntity doctor, CancellationToken ct);
    Task Delete<T>(T id, CancellationToken ct);
}
