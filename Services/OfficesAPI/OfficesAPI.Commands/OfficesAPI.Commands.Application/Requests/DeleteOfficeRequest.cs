namespace OfficesAPI.Commands.Application.Requests;

public record DeleteOfficeRequest(
    Guid Id,
    string? ImageUrl
);