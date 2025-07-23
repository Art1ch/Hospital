using MongoDB.Driver;
using OfficesAPI.Queries.Application.Contracts.Repository;
using OfficesAPI.Queries.Infrastructure.Context;

namespace OfficesAPI.Queries.Infrastructure.Repositories.Abstract;

internal abstract class Repository<TEntity, TId> : IRepository<TEntity, TId>
{
    protected readonly IMongoCollection<TEntity> _collection;

    protected Repository(OfficeDbContext context, string collectionName)
    {
        _collection = context.GetCollection<TEntity>(collectionName);
    }

    protected abstract FilterDefinition<TEntity> GetIdFilter(TId id);
    protected abstract TId GetModelId(TEntity entity);

    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(entity, cancellationToken : cancellationToken);
    }

    public async Task DeleteAsync(TId id, CancellationToken cancellationToken = default)
    {
        var filter = this.GetIdFilter(id);
        await _collection.DeleteOneAsync(filter);
    }

    public async Task<TEntity> GetAsync(TId id, CancellationToken cancellationToken = default)
    {
        var filter = this.GetIdFilter(id);
        var entity = await _collection.Find(filter).FirstAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(TEntity model, CancellationToken cancellationToken = default)
    {
        var id = GetModelId(model);
        var filter = GetIdFilter(id);
        await _collection.ReplaceOneAsync(filter, model, cancellationToken : cancellationToken);
    }
}
