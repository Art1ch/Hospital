using OfficesAPI.Shared.Enum;

namespace OfficesAPI.Queries.Application.RepositoryResults.Office;

public record GetOfficeInfoItem(
    string Address,
    string RegistryPhoneNumber,
    OfficeStatus Status);
