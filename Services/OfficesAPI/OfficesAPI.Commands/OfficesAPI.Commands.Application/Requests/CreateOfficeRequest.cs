using OfficesAPI.Shared.Enum;

namespace OfficesAPI.Commands.Application.Requests.Office;

public record CreateOfficeRequest(
    string Address,
    string RegistryPhoneNumber,
    OfficeStatus Status);
