using DoctorAPI.Core.Entities;

namespace DoctorAPI.Application.Contracts;

public interface ISpecializationRepository
{
    Task<List<SpecializationEntity>> GetAll(int page, int pageSize, CancellationToken ct);
    Task<SpecializationEntity> Get<T>(T id, CancellationToken ct);
    Task<T> Create<T>(T id, CancellationToken ct);
    Task<T> Update<T>(SpecializationEntity doctor, CancellationToken ct);
    Task Delete<T>(T id, CancellationToken ct);
}
