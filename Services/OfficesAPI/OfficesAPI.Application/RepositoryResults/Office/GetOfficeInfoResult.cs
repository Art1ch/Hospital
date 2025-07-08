using OfficesAPI.Core.Enums;

namespace OfficesAPI.Application.RepositoryResults.Office;

public record GetOfficeInfoResult(
    GetOfficeInfoItem Item);

public record GetOfficeInfoItem(
    string Address,
    string RegistryPhoneNumber,
    OfficeStatus Status);
