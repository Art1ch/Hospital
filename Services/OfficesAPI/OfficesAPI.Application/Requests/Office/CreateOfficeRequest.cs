using OfficesAPI.Core.Enums;

namespace OfficesAPI.Application.Requests.Office;

public record CreateOfficeRequest(
    string Address,
    string RegistryPhoneNumber,
    OfficeStatus Status);
