using OfficesAPI.Core.Enums;

namespace OfficesAPI.Application.RepositoryResults.Office;

public record GetAllOfficesResult(
    List<GetAllOfficesCollectionItem> Offices);

public record GetAllOfficesCollectionItem(
    string Address,
    string RegisteryPhoneNumber,
    OfficeStatus Status);
