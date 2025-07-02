using OfficesAPI.Core.Enums;

namespace OfficesAPI.Application.RepositoryResults.DataTransferObjects;

public record GetAllOfficesCollectionItem(
    string Address,
    string RegisteryPhoneNumber,
    OfficeStatus Status);
