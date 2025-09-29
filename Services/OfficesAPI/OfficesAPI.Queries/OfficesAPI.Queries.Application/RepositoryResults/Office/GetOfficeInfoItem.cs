using OfficesAPI.Shared.Enum;

namespace OfficesAPI.Queries.Application.RepositoryResults.Office;

public record GetOfficeInfoItem(
    Guid Id,
    string Address,
    string RegistryPhoneNumber,
    OfficeStatus Status
);