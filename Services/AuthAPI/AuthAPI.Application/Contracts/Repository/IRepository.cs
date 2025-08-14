namespace AuthAPI.Application.Contracts.Repository;

public interface IRepository<TEntity, TId> where TEntity : class
{
    IQueryable<TEntity> GetEntitiesQuery();
    Task<TEntity> GetAsync(TId id, CancellationToken cancellationToken = default);
    Task CreateAsync(TEntity Entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TId id, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity Entity, CancellationToken cancellationToken = default);
}
