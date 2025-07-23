namespace OfficesAPI.Queries.Application.RepositoryResults.Office;

public record GetAllOfficesResult(
    List<GetAllOfficesCollectionItem> Offices);
