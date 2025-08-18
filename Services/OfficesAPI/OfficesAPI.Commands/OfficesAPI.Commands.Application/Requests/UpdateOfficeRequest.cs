using OfficesAPI.Shared.Enum;

namespace OfficesAPI.Commands.Application.Requests.Office;

public record UpdateOfficeRequest(
    Guid Id,
    string Address,
    string RegistryPhoneNumber,
    OfficeStatus Status);
