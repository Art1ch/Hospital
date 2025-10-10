namespace OfficesAPI.Shared.RepositoryResults;

public record GetAllOfficesResult(
    bool HasNextPage,
    List<GetAllOfficesCollectionItem> Offices
);
