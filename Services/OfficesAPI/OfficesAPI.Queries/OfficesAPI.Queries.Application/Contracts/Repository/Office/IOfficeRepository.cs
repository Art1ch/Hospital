using OfficesAPI.Queries.Application.RepositoryResults.Office;
using OfficesAPI.Queries.Core.Entities;
using OfficesAPI.Shared.Enum;

namespace OfficesAPI.Queries.Application.Contracts.Repository.Office;

public interface IOfficeRepository : IRepository<OfficeEntity, Guid>
{
    Task<GetAllOfficesResult> GetAllOfficesAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<GetOfficeInfoResult> GetOfficeInfoAsync(Guid id, CancellationToken cancellationToken = default);
    Task ChangeOfficeStatusAsync(Guid id, OfficeStatus status, CancellationToken cancellationToken = default);
}
