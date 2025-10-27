using OfficesAPI.Shared.Entities;
using OfficesAPI.Shared.Enum;
using OfficesAPI.Shared.RepositoryResults;

namespace OfficesAPI.Queries.Application.Contracts.Repository.Office;

public interface IQueryOfficeRepository : IRepository<OfficeEntity, Guid>
{
    Task<GetAllOfficesResult> GetAllOfficesAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<GetOfficeInfoResult> GetOfficeInfoAsync(Guid id, CancellationToken cancellationToken = default);
    Task ChangeOfficeStatusAsync(Guid id, OfficeStatus status, CancellationToken cancellationToken = default);
}