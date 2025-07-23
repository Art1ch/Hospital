using OfficesAPI.Shared.Enum;

namespace OfficesAPI.Queries.Application.RepositoryResults.Office;

public record GetAllOfficesCollectionItem(
    string Address,
    string RegisteryPhoneNumber,
    OfficeStatus Status);
