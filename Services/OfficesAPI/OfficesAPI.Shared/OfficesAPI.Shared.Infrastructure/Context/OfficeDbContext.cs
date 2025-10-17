using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using OfficesAPI.Shared.Entities;
using OfficesAPI.Shared.Infrastructure.Settings;

namespace OfficesAPI.Shared.Infrastructure.Context;

internal class OfficeDbContext
{
    private readonly IMongoDatabase _database;
    public readonly IMongoClient Client;

    public OfficeDbContext(OfficeDbSettings settings)
    {
        Client = new MongoClient(settings.ConnectionString);
        _database = Client.GetDatabase(settings.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }

    static OfficeDbContext()
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        RegisterClassMaps();
    }

    private static void RegisterClassMaps()
    {
        BsonClassMap.RegisterClassMap<OfficeEntity>(cm =>
        {
            cm.AutoMap();
            cm.SetIgnoreExtraElements(true);
            cm.MapIdProperty(x => x.Id);
        });
    }
}
