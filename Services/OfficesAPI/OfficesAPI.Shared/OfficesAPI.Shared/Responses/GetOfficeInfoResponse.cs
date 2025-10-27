using OfficesAPI.Shared.RepositoryResults;

namespace OfficesAPI.Shared.Responses;

public sealed record GetOfficeInfoResponse(
    GetOfficeInfoResult Office
);
