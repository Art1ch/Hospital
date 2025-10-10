using OfficesAPI.Shared.RepositoryResults;

namespace OfficesAPI.Shared.Responses;

public record GetOfficeInfoResponse(
    GetOfficeInfoResult Office
);
