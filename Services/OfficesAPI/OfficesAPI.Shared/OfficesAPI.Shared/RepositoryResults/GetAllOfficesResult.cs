namespace OfficesAPI.Shared.RepositoryResults;

public sealed record GetAllOfficesResult(
    List<GetAllOfficesCollectionItem> Offices,
    int officesOnPage,
    int totalPages
);
