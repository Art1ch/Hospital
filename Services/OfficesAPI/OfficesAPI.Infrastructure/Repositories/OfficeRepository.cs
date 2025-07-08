using MongoDB.Driver;
using OfficesAPI.Application.Contracts.Repository.Office;
using OfficesAPI.Application.RepositoryResults.DataTransferObjects;
using OfficesAPI.Application.RepositoryResults.Office;
using OfficesAPI.Core.Entities;
using OfficesAPI.Core.Enums;
using OfficesAPI.Infrastructure.Context;
using OfficesAPI.Infrastructure.Repositories.Abstract;

namespace OfficesAPI.Infrastructure.Repositories;

internal class OfficeRepository : Repository<OfficeEntity, Guid>, IOfficeRepository
{
    private const string CollectionName = "Offices";

    public OfficeRepository(OfficeDbContext context) : base(context, CollectionName)
    {
    }

    public async Task ChangeOfficeStatusAsync(Guid id, OfficeStatus status, CancellationToken cancellationToken = default)
    {
        var filter = this.GetIdFilter(id);
        var update = Builders<OfficeEntity>.Update.Set(o => o.Status, status);
        await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }

    public async Task<GetAllOfficesResult> GetAllOfficesAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var projection = Builders<OfficeEntity>.Projection
            .Expression(o => new GetAllOfficesCollectionItem(
                o.Address,
                o.RegistryPhoneNumber,
                o.Status
            ));

        var offices = await _collection
            .Find(FilterDefinition<OfficeEntity>.Empty)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .Project(projection)
            .ToListAsync(cancellationToken);

        return new GetAllOfficesResult(offices);
    }

    public async Task<GetOfficeInfoResult> GetOfficeInfoAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var projection = Builders<OfficeEntity>.Projection
            .Expression(o => new GetOfficeInfoItem(
                o.Address,
                o.RegistryPhoneNumber,
                o.Status
            ));

        var filter = this.GetIdFilter(id);

        var officeInfo = await _collection
            .Find(filter)
            .Project(projection)
            .FirstAsync(cancellationToken);

        return new GetOfficeInfoResult(officeInfo);
    }

    protected override FilterDefinition<OfficeEntity> GetIdFilter(Guid id)
    {
        return Builders<OfficeEntity>.Filter.Eq(o => o.Id, id);
    }

    protected override Guid GetModelId(OfficeEntity entity)
    {
        return entity.Id;
    }
}
