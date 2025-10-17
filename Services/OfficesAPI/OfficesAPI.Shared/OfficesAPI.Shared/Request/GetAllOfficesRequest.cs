namespace OfficesAPI.Shared.Requests;

public sealed record GetAllOfficesRequest(
    int Page,
    int PageSize
);
