using MongoDB.Driver;
using OfficesAPI.Commands.Application.Contracts;
using OfficesAPI.Queries.Application.Contracts.Repository.Office;
using OfficesAPI.Shared.Entities;
using OfficesAPI.Shared.Enum;
using OfficesAPI.Shared.Infrastructure.Context;
using OfficesAPI.Shared.RepositoryResults;

namespace OfficesAPI.Shared.Infrastructure.Repository;

internal class OfficeRepository : BaseRepository<OfficeEntity, Guid>, ICommandOfficeRepository, IQueryOfficeRepository
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
                o.Id,
                o.Address,
                o.RegistryPhoneNumber,
                o.Status,
                o.ImageUrl
            ));

        var offices = await _collection
            .Find(FilterDefinition<OfficeEntity>.Empty)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .Project(projection)
            .ToListAsync(cancellationToken);

        var totalOffices = await _collection
            .CountDocumentsAsync(FilterDefinition<OfficeEntity>.Empty);

        var totalPages = (int)Math.Ceiling((double) totalOffices / pageSize);



        return new GetAllOfficesResult(offices, offices.Count, totalPages);
    }

    public async Task<GetOfficeInfoResult> GetOfficeInfoAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var projection = Builders<OfficeEntity>.Projection
            .Expression(o => new GetOfficeInfoItem(
                o.Id,
                o.Address,
                o.RegistryPhoneNumber,
                o.Status,
                o.ImageUrl
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
