namespace OfficesAPI.Queries.Application.RepositoryResults.Office;

public record GetAllOfficesResult(
    bool HasNextPage,
    List<GetAllOfficesCollectionItem> Offices
);
