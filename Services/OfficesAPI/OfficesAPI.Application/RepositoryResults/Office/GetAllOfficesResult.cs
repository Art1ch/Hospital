namespace OfficesAPI.Application.RepositoryResults.Office;

public record GetAllOfficesResult(
    List<GetAllOfficesCollectionItem> Offices);
