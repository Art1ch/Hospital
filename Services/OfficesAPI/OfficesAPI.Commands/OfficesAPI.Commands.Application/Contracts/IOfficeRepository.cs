using OfficesAPI.Shared.Entities;
using OfficesAPI.Shared.Enum;
using OfficesAPI.Shared.RepositoryResults;

namespace OfficesAPI.Commands.Application.Contracts;

public interface IOfficeRepository : IRepository<OfficeEntity, Guid>
{
    Task<GetAllOfficesResult> GetAllOfficesAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<GetOfficeInfoResult> GetOfficeInfoAsync(Guid id, CancellationToken cancellationToken = default);
    Task ChangeOfficeStatusAsync(Guid id, OfficeStatus status, CancellationToken cancellationToken = default);
}