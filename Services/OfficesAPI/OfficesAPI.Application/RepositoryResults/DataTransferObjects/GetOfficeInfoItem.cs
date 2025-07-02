using OfficesAPI.Core.Enums;

namespace OfficesAPI.Application.RepositoryResults.DataTransferObjects;

public record GetOfficeInfoItem(
    string Address,
    string RegistryPhoneNumber,
    OfficeStatus Status);
