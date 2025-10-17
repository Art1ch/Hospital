using OfficesAPI.Shared.Enum;

namespace OfficesAPI.Shared.RepositoryResults;

public sealed record GetAllOfficesCollectionItem(
    Guid Id,
    string Address,
    string RegistryPhoneNumber,
    OfficeStatus Status,
    string ImageUrl
);
