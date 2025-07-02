using OfficesAPI.Application.RepositoryResults.DataTransferObjects;

namespace OfficesAPI.Application.RepositoryResults.Office;

public record GetAllOfficesResult(
    List<GetAllOfficesCollectionItem> Offices);
