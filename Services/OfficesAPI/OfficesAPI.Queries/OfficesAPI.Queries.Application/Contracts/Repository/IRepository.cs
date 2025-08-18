namespace OfficesAPI.Queries.Application.Contracts.Repository;

public interface IRepository<TEntity, TId>
{
    Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TId id, CancellationToken cancellationToken = default);
    Task<TEntity> GetAsync(TId id, CancellationToken cancellationToken = default);
}
