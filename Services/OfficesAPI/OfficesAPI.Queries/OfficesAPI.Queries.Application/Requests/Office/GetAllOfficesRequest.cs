namespace OfficesAPI.Queries.Application.Requests.Office;

public record GetAllOfficesRequest(
    int Page,
    int PageSize
);
