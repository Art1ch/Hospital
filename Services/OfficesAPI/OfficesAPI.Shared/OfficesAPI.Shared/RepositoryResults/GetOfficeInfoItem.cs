using OfficesAPI.Shared.Enum;

namespace OfficesAPI.Shared.RepositoryResults;

public record GetOfficeInfoItem(
    Guid Id,
    string Address,
    string RegistryPhoneNumber,
    OfficeStatus Status,
    string ImageUrl
);