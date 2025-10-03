using OfficesAPI.Shared.Enum;

namespace OfficesAPI.Queries.Application.RepositoryResults.Office;

public record GetAllOfficesCollectionItem(
    Guid Id,
    string Address,
    string RegistryPhoneNumber,
    OfficeStatus Status,
    string ImageUrl
);
