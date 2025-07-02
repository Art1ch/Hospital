using OfficesAPI.Core.Enums;

namespace OfficesAPI.Application.Requests.Office;

public record UpdateOfficeRequest(
    Guid Id,
    string Address,
    string RegistryPhoneNumber,
    OfficeStatus Status);
