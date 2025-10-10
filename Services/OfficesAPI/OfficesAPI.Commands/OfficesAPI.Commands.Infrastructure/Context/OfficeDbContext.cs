using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using OfficesAPI.Commands.Infrastructure.Settings;
using OfficesAPI.Shared.Entities;

namespace OfficesAPI.Commands.Infrastructure.Context;

internal class OfficeDbContext
{
    private readonly IMongoDatabase _database;
    public readonly IMongoClient Client;

    public OfficeDbContext(IOptions<OfficeDbSettings> options)
    {
        Client = new MongoClient(options.Value.ConnectionString);
        _database = Client.GetDatabase(options.Value.DatabaseName);
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